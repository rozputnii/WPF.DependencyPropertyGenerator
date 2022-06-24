﻿namespace H.Generators;

public readonly record struct DependencyPropertyData(
    string Name,
    string Type,
    bool IsValueType,
    bool IsSpecialType,
    string? DefaultValue,
    string? DefaultValueDocumentation,
    string? Description,
    string? Category,
    string? TypeConverter,
    string? Bindable,
    string? DesignerSerializationVisibility,
    string? CLSCompliant,
    string? Localizability,
    string? BrowsableForType,
    bool IsBrowsableForTypeSpecialType,
    string? XmlDocumentation,
    string? SetterXmlDocumentation,
    string? GetterXmlDocumentation,
    bool AffectsMeasure,
    bool AffectsArrange,
    bool AffectsParentMeasure,
    bool AffectsParentArrange,
    bool AffectsRender,
    bool Inherits,
    bool OverridesInheritanceBehavior,
    bool NotDataBindable,
    bool BindsTwoWayByDefault,
    bool Journal,
    bool SubPropertiesDoNotAffectRender,
    bool IsAnimationProhibited,
    string? DefaultUpdateSourceTrigger,
    bool Coerce,
    bool Validate);