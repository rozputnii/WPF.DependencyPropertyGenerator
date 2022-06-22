﻿using System.Collections.Immutable;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace H.Generators;

[Generator]
public class DependencyPropertyGenerator : IIncrementalGenerator
{
    #region Constants

    public const string Name = nameof(DependencyPropertyGenerator);
    public const string Id = "DPG";

    private const string AttachedDependencyPropertyAttribute = "DependencyPropertyGenerator.AttachedDependencyPropertyAttribute";
    private const string DependencyPropertyAttribute = "DependencyPropertyGenerator.DependencyPropertyAttribute";
    private const string RoutedEventAttribute = "DependencyPropertyGenerator.RoutedEventAttribute";

    #endregion

    #region Methods

    private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var syntax = (ClassDeclarationSyntax)context.Node;

        foreach (var attributeListSyntax in syntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                var fullName = attributeContainingTypeSymbol.ToDisplayString();
                if (fullName.StartsWith(AttachedDependencyPropertyAttribute) ||
                    fullName.StartsWith(DependencyPropertyAttribute) ||
                    fullName.StartsWith(RoutedEventAttribute))
                {
                    return syntax;
                }
            }
        }

        return null;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classes = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: static (context, _) => GetSemanticTargetForGeneration(context))
            .Where(static syntax => syntax is not null);

        var compilationAndClasses = context.CompilationProvider
            .Combine(context.AnalyzerConfigOptionsProvider)
            .Combine(classes.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses,
            static (context, source) => Execute(source.Left.Left, source.Left.Right, source.Right!, context));
    }

    private static void Execute(
        Compilation compilation,
        AnalyzerConfigOptionsProvider options,
        ImmutableArray<ClassDeclarationSyntax> classSyntaxes,
        SourceProductionContext context)
    {
        if (classSyntaxes.IsDefaultOrEmpty)
        {
            return;
        }
        
        try
        {
            var constants = options.GetGlobalOption("DefineConstants", prefix: Name) ?? string.Empty;
            var useWpf = bool.Parse(options.GetGlobalOption("UseWPF") ?? bool.FalseString) || constants.Contains("HAS_WPF");
            var useWinUI = bool.Parse(options.GetGlobalOption("UseWinUI") ?? bool.FalseString) || constants.Contains("HAS_WINUI");
            var useUwp = constants.Contains("WINDOWS_UWP") || constants.Contains("HAS_UWP");
            var useUno = constants.Contains("HAS_UNO");
            var useUnoWinUI = constants.Contains("HAS_UNO_WINUI") || (constants.Contains("HAS_UNO") && constants.Contains("HAS_WINUI"));
            var platform = (useWpf, useUwp, useWinUI, useUno, useUnoWinUI) switch
            {
                (_, _, _, _, true) => Platform.UnoWinUI,
                (_, _, _, true, _) => Platform.Uno,
                (_, _, true, _, _) => Platform.WinUI,
                (_, true, _, _, _) => Platform.UWP,
                (true, _, _, _, _) => Platform.WPF,
                _ =>                  Platform.Undefined,
            };

            var classes = GetTypesToGenerate(compilation, platform, classSyntaxes, context.CancellationToken);
            foreach (var @class in classes)
            {
                if (@class.DependencyProperties.Any())
                {
                    context.AddTextSource(
                        hintName: $"{@class.Name}_DependencyProperties.generated.cs",
                        text: SourceGenerationHelper.GenerateDependencyProperty(@class));
                }
                if (@class.AttachedDependencyProperties.Any())
                {
                    context.AddTextSource(
                        hintName: $"{@class.Name}_AttachedDependencyProperties.generated.cs",
                        text: SourceGenerationHelper.GenerateAttachedDependencyProperty(@class));
                }
                if (platform == Platform.WPF && @class.RoutedEvents.Any())
                {
                    context.AddTextSource(
                        hintName: $"{@class.Name}_RoutedEvents.generated.cs",
                        text: SourceGenerationHelper.GenerateRoutedEvent(@class));
                }
            }
        }
        catch (Exception exception)
        {
            context.ReportException(
                id: "001",
                exception: exception,
                prefix: Id);
        }
    }

    private static IReadOnlyCollection<ClassData> GetTypesToGenerate(
        Compilation compilation,
        Platform platform,
        IEnumerable<ClassDeclarationSyntax> classes,
        CancellationToken cancellationToken)
    {
        var values = new List<ClassData>();
        foreach (var group in classes.GroupBy(@class => GetFullClassName(compilation, @class)))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var @class = group.First();

            var semanticModel = compilation.GetSemanticModel(@class.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(
                @class, cancellationToken) is not INamedTypeSymbol classSymbol)
            {
                continue;
            }

            var fullClassName = classSymbol.ToString();
            var @namespace = fullClassName.Substring(0, fullClassName.LastIndexOf('.'));
            var className = fullClassName.Substring(fullClassName.LastIndexOf('.') + 1);
            var classModifiers = classSymbol.IsStatic ? " static" : string.Empty;

            var dependencyProperties = new List<DependencyPropertyData>();
            var attachedDependencyProperties = new List<DependencyPropertyData>();
            var routedEvents = new List<RoutedEventData>();
            foreach (var (attributeSyntax, attribute) in group
                .SelectMany(static list => list.AttributeLists)
                .SelectMany(static list => list.Attributes)
                .Zip(classSymbol.GetAttributes(), static (a, b) => (a, b)))
            {
                var attributeClass = attribute.AttributeClass?.ToDisplayString() ?? string.Empty;
                if (attributeClass.StartsWith(RoutedEventAttribute))
                {
                    var name = attribute.ConstructorArguments[0].Value as string ?? string.Empty;
                    var strategy = attribute.ConstructorArguments[1].Value?.ToString() ?? string.Empty;
                    var type =
                        GetGenericTypeArgumentFromAttributeData(attribute, 0)?.ToDisplayString() ??
                        GetPropertyFromAttributeData(attribute, nameof(RoutedEventData.Type))?.Value?.ToString();

                    var description = GetPropertyFromAttributeData(attribute, nameof(RoutedEventData.Description))?.Value?.ToString();
                    var category = GetPropertyFromAttributeData(attribute, nameof(RoutedEventData.Category))?.Value?.ToString();

                    var xmlDocumentation = GetPropertyFromAttributeData(attribute, nameof(RoutedEventData.XmlDocumentation))?.Value?.ToString();
                    var eventXmlDocumentation = GetPropertyFromAttributeData(attribute, nameof(RoutedEventData.EventXmlDocumentation))?.Value?.ToString();

                    var value = new RoutedEventData(
                        Name: name,
                        Strategy: strategy,
                        Type: type,
                        Description: description,
                        Category: category,
                        XmlDocumentation: xmlDocumentation,
                        EventXmlDocumentation: eventXmlDocumentation);
                    
                    routedEvents.Add(value);
                    continue;
                }
                else
                {
                    var name = attribute.ConstructorArguments[0].Value as string ?? string.Empty;
                    var type =
                        GetGenericTypeArgumentFromAttributeData(attribute, 0)?.ToDisplayString() ??
                        attribute.ConstructorArguments.ElementAtOrDefault(1).Value?.ToString() ??
                        string.Empty;
                    var isValueType =
                        GetGenericTypeArgumentFromAttributeData(attribute, 0)?.IsValueType ??
                        attribute.ConstructorArguments.ElementAtOrDefault(1).Type?.IsValueType ??
                        true;
                    var isSpecialType =
                        IsSpecialType(GetGenericTypeArgumentFromAttributeData(attribute, 0)) ??
                        IsSpecialType(attribute.ConstructorArguments.ElementAtOrDefault(1).Type) ??
                        false;
                    var defaultValue =
                        GetPropertyFromAttributeData(attribute, "DefaultValueExpression")?.Value?.ToString() ??
                        GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.DefaultValue));
                    var browsableForType =
                        GetGenericTypeArgumentFromAttributeData(attribute, 1)?.ToDisplayString() ??
                        GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.BrowsableForType))?.Value?.ToString();
                    var isBrowsableForTypeSpecialType =
                        IsSpecialType(GetGenericTypeArgumentFromAttributeData(attribute, 1)) ??
                        IsSpecialType(GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.BrowsableForType))?.Type) ??
                        false;

                    var description = GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.Description))?.Value?.ToString();
                    var category = GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.Category))?.Value?.ToString();
                    var typeConverter = GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.TypeConverter))?.Value?.ToString();
                    var clsCompliant = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.CLSCompliant));
                    var localizability = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.Localizability))?.Replace("Localizability.", string.Empty);

                    var xmlDocumentation = GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.XmlDocumentation))?.Value?.ToString();
                    var propertyXmlDocumentation = GetPropertyFromAttributeData(attribute, "PropertyXmlDocumentation")?.Value?.ToString();
                    var getterXmlDocumentation = GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.GetterXmlDocumentation))?.Value?.ToString();
                    var setterXmlDocumentation = GetPropertyFromAttributeData(attribute, nameof(DependencyPropertyData.SetterXmlDocumentation))?.Value?.ToString();

                    var affectsMeasure = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.AffectsMeasure)) ?? bool.FalseString;
                    var affectsArrange = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.AffectsArrange)) ?? bool.FalseString;
                    var affectsParentMeasure = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.AffectsParentMeasure)) ?? bool.FalseString;
                    var affectsParentArrange = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.AffectsParentArrange)) ?? bool.FalseString;
                    var affectsRender = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.AffectsRender)) ?? bool.FalseString;
                    var inherits = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.Inherits)) ?? bool.FalseString;
                    var overridesInheritanceBehavior = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.OverridesInheritanceBehavior)) ?? bool.FalseString;
                    var notDataBindable = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.NotDataBindable)) ?? bool.FalseString;
                    var bindsTwoWayByDefault = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.BindsTwoWayByDefault)) ?? bool.FalseString;
                    var journal = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.Journal)) ?? bool.FalseString;
                    var subPropertiesDoNotAffectRender = GetPropertyFromAttributeSyntax(attributeSyntax, nameof(DependencyPropertyData.SubPropertiesDoNotAffectRender)) ?? bool.FalseString;

                    var value = new DependencyPropertyData(
                        Name: name,
                        Type: type,
                        IsValueType: isValueType,
                        IsSpecialType: isSpecialType,
                        DefaultValue: defaultValue,
                        Description: description,
                        Category: category,
                        TypeConverter: typeConverter,
                        CLSCompliant: clsCompliant,
                        Localizability: localizability,
                        BrowsableForType: browsableForType,
                        IsBrowsableForTypeSpecialType: isBrowsableForTypeSpecialType,
                        XmlDocumentation: xmlDocumentation,
                        GetterXmlDocumentation: getterXmlDocumentation ?? propertyXmlDocumentation,
                        SetterXmlDocumentation: setterXmlDocumentation,
                        AffectsMeasure: bool.Parse(affectsMeasure),
                        AffectsArrange: bool.Parse(affectsArrange),
                        AffectsParentMeasure: bool.Parse(affectsParentMeasure),
                        AffectsParentArrange: bool.Parse(affectsParentArrange),
                        AffectsRender: bool.Parse(affectsRender),
                        Inherits: bool.Parse(inherits),
                        OverridesInheritanceBehavior: bool.Parse(overridesInheritanceBehavior),
                        NotDataBindable: bool.Parse(notDataBindable),
                        BindsTwoWayByDefault: bool.Parse(bindsTwoWayByDefault),
                        Journal: bool.Parse(journal),
                        SubPropertiesDoNotAffectRender: bool.Parse(subPropertiesDoNotAffectRender));

                    if (attributeClass.StartsWith(DependencyPropertyAttribute))
                    {
                        dependencyProperties.Add(value);
                    }
                    else if (attributeClass.StartsWith(AttachedDependencyPropertyAttribute))
                    {
                        attachedDependencyProperties.Add(value);
                    }
                }
            }

            values.Add(new ClassData(
                Namespace: @namespace,
                Name: className,
                FullName: fullClassName,
                Modifiers: classModifiers,
                Platform: platform,
                DependencyProperties: dependencyProperties,
                AttachedDependencyProperties: attachedDependencyProperties,
                RoutedEvents: routedEvents));
        }

        return values;
    }

    private static string? GetFullClassName(Compilation compilation, ClassDeclarationSyntax classDeclarationSyntax)
    {
        var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
        if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
        {
            return null;
        }

        return classSymbol.ToString();
    }

    private static bool? IsSpecialType(ITypeSymbol? symbol)
    {
        if (symbol == null)
        {
            return null;
        }

        return symbol.SpecialType != SpecialType.None;
    }

    private static ITypeSymbol? GetGenericTypeArgumentFromAttributeData(AttributeData data, int position)
{
        return data.AttributeClass?.TypeArguments.ElementAtOrDefault(position);
    }

    private static TypedConstant? GetPropertyFromAttributeData(AttributeData data, string name)
{
        return data.NamedArguments
            .FirstOrDefault(pair => pair.Key == name)
            .Value;
    }

    private static string? GetPropertyFromAttributeSyntax(AttributeSyntax syntax, string name)
    {
        return syntax.ArgumentList?.Arguments
            .FirstOrDefault(pair => pair.NameEquals?.ToFullString().StartsWith(name) == true)?
            .Expression
            .ToFullString();
    }

    #endregion
}
