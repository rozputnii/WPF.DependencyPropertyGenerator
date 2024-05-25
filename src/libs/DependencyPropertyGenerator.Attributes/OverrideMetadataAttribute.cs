﻿// ReSharper disable RedundantNameQualifier
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#nullable enable

namespace DependencyPropertyGenerator;

/// <summary>
/// Overrides dependency property metadata using DependencyProperty.OverrideMetadata. <br/>
/// Metadata override behavior: <seealso href="https://docs.microsoft.com/en-us/dotnet/desktop/wpf/properties/framework-property-metadata?view=netdesktop-6.0#metadata-override-behavior"/>
/// </summary>
[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = true)]
[global::System.Diagnostics.Conditional("DEPENDENCY_PROPERTY_GENERATOR_ATTRIBUTES")]
public sealed class OverrideMetadataAttribute : global::System.Attribute
{
    /// <summary>
    /// Name of this dependency property.
    /// </summary>
	public string Name { get; }

    /// <summary>
    /// Type of this dependency property.
    /// </summary>
    public global::System.Type Type { get; }

    /// <summary>
    /// Default value of this dependency property. <br/>
    /// If you need to pass a new() expression, use <see cref="DefaultValueExpression"/>. <br/>
    /// Default - <see langword="default(type)"/>.
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Default value expression of this dependency property. <br/>
    /// Used to pass a new() expression to an initializer. <br/>
    /// Default - <see langword="null"/>.
    /// </summary>
    public string? DefaultValueExpression { get; set; }

    /// <summary>
    /// The property will create through RegisterReadOnly (if the platform supports it) and 
    /// the property setter will contain the protected modifier. <br/>
    /// Default - <see langword="false"/>.
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <summary>
    /// For values other than default(type), will bind/rebind/remove the 
    /// On{Name}Changed_{EventName}(object sender, Args args) handler for the specified event. <br/>
    /// It is recommended to specify as nameof(UIElement.Drop). <br/>
    /// Default - string.Empty.
    /// </summary>
    public string BindEvent { get; set; } = string.Empty;

    /// <summary>
    /// For values other than default(type), will bind/rebind/remove the 
    /// On{Name}Changed_{EventName}(object sender, Args args) handler for the specified event. <br/>
    /// It is recommended to specify as nameof(UIElement.Drop). <br/>
    /// Default - <see langword="null"/>.
    /// </summary>
    public string[]? BindEvents { get; set; }

    /// <summary>
    /// Allows to set a custom name for the OnChanged method call, allowing this method to be non-partial.
    /// </summary>
    public string OnChanged { get; set; } = string.Empty;

    /// <summary>
    /// WPF: The measure pass of layout compositions is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsMeasure { get; set; }

    /// <summary>
    /// WPF: The arrange pass of layout composition is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsArrange { get; set; }

    /// <summary>
    /// WPF: The measure pass on the parent element is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsParentMeasure { get; set; }

    /// <summary>
    /// WPF: The arrange pass on the parent element is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsParentArrange { get; set; }

    /// <summary>
    /// WPF: Some aspect of rendering or layout composition (other than measure or arrange) is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsRender { get; set; }

    /// <summary>
    /// WPF: The values of this dependency property are inherited by child elements.
    /// </summary>
    public bool Inherits { get; set; }

    /// <summary>
    /// WPF: The values of this dependency property span separated trees for purposes of property value inheritance.
    /// </summary>
    public bool OverridesInheritanceBehavior { get; set; }

    /// <summary>
    /// WPF: Data binding to this dependency property is not allowed.
    /// </summary>
    public bool NotDataBindable { get; set; }

    /// <summary>
    /// WPF: The values of this dependency property should be saved or restored by journaling processes, or when navigating by Uniform resource identifiers (URIs).
    /// </summary>
    public bool Journal { get; set; }

    /// <summary>
    /// WPF: The sub properties on the value of this dependency property do not affect any aspect of rendering.
    /// </summary>
    public bool SubPropertiesDoNotAffectRender { get; set; }

    /// <summary>
    /// WPF: true to prevent the property system from animating the property that this metadata
    /// is applied to. Such properties will raise a run-time exception originating from
    /// the property system if animations of them are attempted. The default is false.
    /// </summary>
    public bool IsAnimationProhibited { get; set; }

    /// <summary>
    /// WPF: The System.Windows.Data.UpdateSourceTrigger to use when bindings for this property
    /// are applied that have their System.Windows.Data.UpdateSourceTrigger set to 
    /// System.Windows.Data.UpdateSourceTrigger.Default.
    /// </summary>
    public SourceTrigger DefaultUpdateSourceTrigger { get; set; }

    /// <summary>
    /// Avalonia/MAUI: Default BindingMode. <br/>
    /// WPF: Only Default/TwoWay is supported. <br/>
    /// Default - <see cref="DefaultBindingMode.Default"/>.
    /// </summary>
    public DefaultBindingMode DefaultBindingMode { get; set; } = DefaultBindingMode.Default;

    /// <summary>
    /// WPF: partial method for coerceValueCallback will be created.
    /// </summary>
    public bool Coerce { get; set; }

    /// <summary>
    /// WPF: partial method for validateValueCallback will be created.
    /// </summary>
    public bool Validate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <exception cref="global::System.ArgumentNullException"></exception>
    public OverrideMetadataAttribute(
        string name,
        global::System.Type type)
    {
        Name = name ?? throw new global::System.ArgumentNullException(nameof(name));
        Type = type ?? throw new global::System.ArgumentNullException(nameof(type));
    }
}

/// <summary>
/// Will override dependency property metadata using DependencyProperty.OverrideMetadata. <br/>
/// Metadata override behavior: <seealso href="https://docs.microsoft.com/en-us/dotnet/desktop/wpf/properties/framework-property-metadata?view=netdesktop-6.0#metadata-override-behavior"/>
/// </summary>
/// <typeparam name="T">Type of this dependency property.</typeparam>
[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = true)]
[global::System.Diagnostics.Conditional("DEPENDENCY_PROPERTY_GENERATOR_ATTRIBUTES")]
internal sealed class OverrideMetadataAttribute<T> : global::System.Attribute
{
    /// <summary>
    /// Name of this dependency property.
    /// </summary>
	public string Name { get; }

    /// <summary>
    /// Type of this dependency property.
    /// </summary>
    public global::System.Type Type { get; }

    /// <summary>
    /// Default value of this dependency property. <br/>
    /// If you need to pass a new() expression, use <see cref="DefaultValueExpression"/>. <br/>
    /// Default - <see langword="default(type)"/>.
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Default value expression of this dependency property. <br/>
    /// Used to pass a new() expression to an initializer. <br/>
    /// Default - <see langword="null"/>.
    /// </summary>
    public string? DefaultValueExpression { get; set; }

    /// <summary>
    /// The property will create through RegisterReadOnly (if the platform supports it) and 
    /// the property setter will contain the protected modifier. <br/>
    /// Default - <see langword="false"/>.
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <summary>
    /// For values other than default(type), will bind/rebind/remove the 
    /// On{Name}Changed_{EventName}(object sender, Args args) handler for the specified event. <br/>
    /// It is recommended to specify as nameof(UIElement.Drop). <br/>
    /// Default - string.Empty.
    /// </summary>
    public string BindEvent { get; set; } = string.Empty;

    /// <summary>
    /// For values other than default(type), will bind/rebind/remove the 
    /// On{Name}Changed_{EventName}(object sender, Args args) handler for the specified event. <br/>
    /// It is recommended to specify as nameof(UIElement.Drop). <br/>
    /// Default - <see langword="null"/>.
    /// </summary>
    public string[]? BindEvents { get; set; }

    /// <summary>
    /// Allows to set a custom name for the OnChanged method call, allowing this method to be non-partial.
    /// </summary>
    public string OnChanged { get; set; } = string.Empty;

    /// <summary>
    /// WPF: The measure pass of layout compositions is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsMeasure { get; set; }

    /// <summary>
    /// WPF: The arrange pass of layout composition is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsArrange { get; set; }

    /// <summary>
    /// WPF: The measure pass on the parent element is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsParentMeasure { get; set; }

    /// <summary>
    /// WPF: The arrange pass on the parent element is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsParentArrange { get; set; }

    /// <summary>
    /// WPF: Some aspect of rendering or layout composition (other than measure or arrange) is affected by value changes to this dependency property.
    /// </summary>
    public bool AffectsRender { get; set; }

    /// <summary>
    /// WPF: The values of this dependency property are inherited by child elements.
    /// </summary>
    public bool Inherits { get; set; }

    /// <summary>
    /// WPF: The values of this dependency property span separated trees for purposes of property value inheritance.
    /// </summary>
    public bool OverridesInheritanceBehavior { get; set; }

    /// <summary>
    /// WPF: Data binding to this dependency property is not allowed.
    /// </summary>
    public bool NotDataBindable { get; set; }

    /// <summary>
    /// WPF: The values of this dependency property should be saved or restored by journaling processes, or when navigating by Uniform resource identifiers (URIs).
    /// </summary>
    public bool Journal { get; set; }

    /// <summary>
    /// WPF: The sub properties on the value of this dependency property do not affect any aspect of rendering.
    /// </summary>
    public bool SubPropertiesDoNotAffectRender { get; set; }

    /// <summary>
    /// WPF: true to prevent the property system from animating the property that this metadata
    /// is applied to. Such properties will raise a run-time exception originating from
    /// the property system if animations of them are attempted. The default is false.
    /// </summary>
    public bool IsAnimationProhibited { get; set; }

    /// <summary>
    /// WPF: The System.Windows.Data.UpdateSourceTrigger to use when bindings for this property
    /// are applied that have their System.Windows.Data.UpdateSourceTrigger set to 
    /// System.Windows.Data.UpdateSourceTrigger.Default.
    /// </summary>
    public SourceTrigger DefaultUpdateSourceTrigger { get; set; }

    /// <summary>
    /// Avalonia/MAUI: Default BindingMode. <br/>
    /// WPF: Only Default/TwoWay is supported. <br/>
    /// Default - <see cref="DefaultBindingMode.Default"/>.
    /// </summary>
    public DefaultBindingMode DefaultBindingMode { get; set; } = DefaultBindingMode.Default;

    /// <summary>
    /// WPF: partial method for coerceValueCallback will be created.
    /// </summary>
    public bool Coerce { get; set; }

    /// <summary>
    /// WPF: partial method for validateValueCallback will be created.
    /// </summary>
    public bool Validate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="global::System.ArgumentNullException"></exception>
    public OverrideMetadataAttribute(
        string name)
    {
        Name = name ?? throw new global::System.ArgumentNullException(nameof(name));
        Type = typeof(T);
    }
}