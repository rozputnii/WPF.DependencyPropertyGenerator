﻿//HintName: H.Generators.IntegrationTests.MyControl.Properties.ReadOnlyProperty.g.cs

#nullable enable

namespace H.Generators.IntegrationTests
{
    public partial class MyControl
    {
        /// <summary>
        /// Identifies the <see cref="ReadOnlyProperty"/> dependency property.<br/>
        /// Default value: default(bool)
        /// </summary>
        public static readonly global::Windows.UI.Xaml.DependencyProperty ReadOnlyPropertyProperty =
            global::Windows.UI.Xaml.DependencyProperty.Register(
                name: "ReadOnlyProperty",
                propertyType: typeof(bool),
                ownerType: typeof(global::H.Generators.IntegrationTests.MyControl),
                typeMetadata: new global::Windows.UI.Xaml.PropertyMetadata(
                    defaultValue: default(bool),
                    propertyChangedCallback: null));

        /// <summary>
        /// Default value: default(bool)
        /// </summary>
        public bool ReadOnlyProperty
        {
            get => (bool)GetValue(ReadOnlyPropertyProperty);
            protected set => SetValue(ReadOnlyPropertyProperty, value);
        }

        partial void OnReadOnlyPropertyChanged();
        partial void OnReadOnlyPropertyChanged(bool newValue);
        partial void OnReadOnlyPropertyChanged(bool oldValue, bool newValue);
    }
}