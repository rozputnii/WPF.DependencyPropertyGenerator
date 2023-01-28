﻿//HintName: GridHelpers.AttachedProperties.RowCount.generated.cs

#nullable enable

namespace H.Generators.IntegrationTests
{
    public static partial class GridHelpers
    {
        /// <summary>
        /// Identifies the RowCount dependency property.<br/>
        /// Default value: -1
        /// </summary>
        public static readonly global::Windows.UI.Xaml.DependencyProperty RowCountProperty =
            global::Windows.UI.Xaml.DependencyProperty.RegisterAttached(
                name: "RowCount",
                propertyType: typeof(int),
                ownerType: typeof(global::H.Generators.IntegrationTests.GridHelpers),
                typeMetadata: new global::Windows.UI.Xaml.PropertyMetadata(
                    defaultValue: (int)-1,
                    propertyChangedCallback: static (sender, args) =>
                    {
                        OnRowCountChanged(
                            (global::Windows.UI.Xaml.Controls.Grid)sender,
                            (int)args.NewValue);
                    }));

        /// <summary>
        /// Default value: -1
        /// </summary>
        public static void SetRowCount(global::Windows.UI.Xaml.DependencyObject element, int value)
        {
            element = element ?? throw new global::System.ArgumentNullException(nameof(element));

            element.SetValue(RowCountProperty, value);
        }

        /// <summary>
        /// Default value: -1
        /// </summary>
        public static int GetRowCount(global::Windows.UI.Xaml.DependencyObject element)
        {
            element = element ?? throw new global::System.ArgumentNullException(nameof(element));

            return (int)element.GetValue(RowCountProperty);
        }

    }
}