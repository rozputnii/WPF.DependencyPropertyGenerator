﻿//HintName: Namespace2.MyControl.Properties.MyProperty.g.cs

#nullable enable

namespace Namespace2
{
    public partial class MyControl
    {
        /// <summary>
        /// Identifies the <see cref="MyProperty"/> dependency property.<br/>
        /// Default value: default(int)
        /// </summary>
        public static readonly global::Windows.UI.Xaml.DependencyProperty MyPropertyProperty =
            global::Windows.UI.Xaml.DependencyProperty.Register(
                name: "MyProperty",
                propertyType: typeof(int),
                ownerType: typeof(global::Namespace2.MyControl),
                typeMetadata: new global::Windows.UI.Xaml.PropertyMetadata(
                    defaultValue: default(int),
                    propertyChangedCallback: null));

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