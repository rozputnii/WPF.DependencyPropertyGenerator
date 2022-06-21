﻿//HintName: MainWindow_DependencyProperties.generated.cs

#nullable enable

namespace H.Generators.IntegrationTests
{
    public partial class MainWindow
    {
        /// <summary></summary>
        public static readonly global::System.Windows.DependencyProperty IsSpinningProperty =
            global::System.Windows.DependencyProperty.Register(
                name: "IsSpinning",
                propertyType: typeof(bool),
                ownerType: typeof(MainWindow),
                typeMetadata: new global::System.Windows.FrameworkPropertyMetadata(
                    defaultValue: default(bool),
                    flags: global::System.Windows.FrameworkPropertyMetadataOptions.None,
                    propertyChangedCallback: static (sender, args) => ((MainWindow)sender).OnIsSpinningChanged((bool)args.OldValue, (bool)args.NewValue)));

        /// <summary></summary>
        public bool IsSpinning
        {
            get => (bool)GetValue(IsSpinningProperty);
            set => SetValue(IsSpinningProperty, value);
        }

        partial void OnIsSpinningChanged(bool oldValue, bool newValue);

        /// <summary></summary>
        public static readonly global::System.Windows.DependencyProperty IsSpinning2Property =
            global::System.Windows.DependencyProperty.Register(
                name: "IsSpinning2",
                propertyType: typeof(bool),
                ownerType: typeof(MainWindow),
                typeMetadata: new global::System.Windows.FrameworkPropertyMetadata(
                    defaultValue: default(bool),
                    flags: global::System.Windows.FrameworkPropertyMetadataOptions.None,
                    propertyChangedCallback: static (sender, args) => ((MainWindow)sender).OnIsSpinning2Changed((bool)args.OldValue, (bool)args.NewValue)));

        /// <summary></summary>
        public bool IsSpinning2
        {
            get => (bool)GetValue(IsSpinning2Property);
            set => SetValue(IsSpinning2Property, value);
        }

        partial void OnIsSpinning2Changed(bool oldValue, bool newValue);
    }
}