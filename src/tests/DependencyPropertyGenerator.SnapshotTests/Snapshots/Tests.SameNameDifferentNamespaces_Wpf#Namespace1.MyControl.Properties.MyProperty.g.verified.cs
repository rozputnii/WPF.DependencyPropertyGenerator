﻿//HintName: Namespace1.MyControl.Properties.MyProperty.g.cs

#nullable enable

namespace Namespace1
{
    public partial class MyControl
    {
        /// <summary>
        /// Identifies the <see cref="MyProperty"/> dependency property.<br/>
        /// Default value: default(int)
        /// </summary>
        public static readonly global::System.Windows.DependencyProperty MyPropertyProperty =
            global::System.Windows.DependencyProperty.Register(
                name: "MyProperty",
                propertyType: typeof(int),
                ownerType: typeof(global::Namespace1.MyControl),
                typeMetadata: new global::System.Windows.FrameworkPropertyMetadata(
                    defaultValue: default(int),
                    flags: global::System.Windows.FrameworkPropertyMetadataOptions.None,
                    propertyChangedCallback: null,
                    coerceValueCallback: null,
                    isAnimationProhibited: false),
                validateValueCallback: null);

        /// <summary>
        /// Default value: default(int)
        /// </summary>
        public int MyProperty
        {
            get => (int)GetValue(MyPropertyProperty);
            set => SetValue(MyPropertyProperty, value);
        }

        partial void OnMyPropertyChanged();
        partial void OnMyPropertyChanged(int newValue);
        partial void OnMyPropertyChanged(int oldValue, int newValue);
    }
}