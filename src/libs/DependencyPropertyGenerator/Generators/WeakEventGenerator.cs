﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H.Generators;

[Generator]
public class WeakEventGenerator : IIncrementalGenerator
{
    #region Constants

    private const string Name = nameof(WeakEventGenerator);
    private const string Id = "WEG";

    #endregion

    #region Methods

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var framework = context.DetectFramework(Name, Id);
        
        context.RegisterSourceOutputOfFiles(
            context.SyntaxProvider
                .ForAttributeWithMetadataName("DependencyPropertyGenerator.WeakEventAttribute")
                .SelectManyAllAttributesOfCurrentClassSyntax()
                .Combine(framework)
                .PrepareData(PrepareData, context, Id)
                .SafeSelect(GetSourceCode, context, prefix: Id));
        context.RegisterSourceOutputOfFiles(
            context.SyntaxProvider
                .ForAttributeWithMetadataName("DependencyPropertyGenerator.WeakEventAttribute`1")
                .SelectManyAllAttributesOfCurrentClassSyntax()
                .Combine(framework)
                .PrepareData(PrepareData, context, Id)
                .SafeSelect(GetSourceCode, context, prefix: Id));
    }

    private static (ClassData Class, EventData Event)? PrepareData(
        Framework framework,
        (SemanticModel SemanticModel, AttributeData AttributeData, ClassDeclarationSyntax ClassSyntax, INamedTypeSymbol ClassSymbol) tuple)
    {
        if (framework is not (Framework.Maui or Framework.Wpf))
        {
            return null;
        }

        var (_, attribute, _, classSymbol) = tuple;
        var classData = classSymbol.GetClassData(framework);
        var eventData = attribute.GetEventData(isStaticClass: classData.IsStatic);

        return (classData, eventData);
    }
    
    private static FileWithName GetSourceCode((ClassData Class, EventData Event) data)
    {
        return new FileWithName(
            Name: $"{data.Class.Name}.WeakEvents.{data.Event.Name}.generated.cs",
            Text: SourceGenerationHelper.GenerateWeakEvent(data.Class, data.Event));
    }

    #endregion
}
