﻿//HintName: H.Generators.IntegrationTests.MyGrid.Properties.IsSpinning.g.cs

#nullable enable

namespace H.Generators.IntegrationTests
{
    partial class MyGrid
    {
        /// <summary>
        /// Identifies the <see cref="IsSpinning"/> dependency property.<br/>
        /// Default value: default(bool)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("DependencyPropertyGenerator", "1.3.3.0")]
        public static readonly global::Avalonia.DirectProperty<global::H.Generators.IntegrationTests.MyGrid, bool> IsSpinningProperty =
            global::Avalonia.AvaloniaProperty.RegisterDirect<global::H.Generators.IntegrationTests.MyGrid, bool>(
                name: "IsSpinning",
                getter: static sender => sender.IsSpinning,
                setter: static (sender, value) => sender.IsSpinning = value,
                unsetValue: default(bool),
                defaultBindingMode: global::Avalonia.Data.BindingMode.OneWay,
                enableDataValidation: false);

        private bool _isSpinning = default(bool);

        /// <summary>
        /// Default value: default(bool)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("DependencyPropertyGenerator", "1.3.3.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public bool IsSpinning
        {
            get => _isSpinning;
            private set
            {
                var oldValue = _isSpinning;
                SetAndRaise(IsSpinningProperty, ref _isSpinning, value);
                OnIsSpinningChanged();
                OnIsSpinningChanged(
                    (bool)value);
                OnIsSpinningChanged(
                    (bool)oldValue,
                    (bool)value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("DependencyPropertyGenerator", "1.3.3.0")]
        partial void OnIsSpinningChanged();
        [global::System.CodeDom.Compiler.GeneratedCode("DependencyPropertyGenerator", "1.3.3.0")]
        partial void OnIsSpinningChanged(bool newValue);
        [global::System.CodeDom.Compiler.GeneratedCode("DependencyPropertyGenerator", "1.3.3.0")]
        partial void OnIsSpinningChanged(bool oldValue, bool newValue);
    }
}