﻿namespace H.Generators.SnapshotTests;

[TestClass]
public class DependencyPropertyGeneratorSnapshotTests : VerifyBase
{
    [TestMethod]
    public Task GeneratesCorrectly()
    {
        return this.CheckSourceAsync(@"
using DependencyPropertyGenerator;
using System.Windows;
using System.Windows.Controls;

#nullable enable

namespace H.Generators.IntegrationTests;

[DependencyProperty<bool>(""IsSpinning"", DefaultValue = true, Category = ""Category"", Description = ""Description"")]
public partial class MainWindow : Window
{
    // Optional
    partial void OnIsSpinningChanged(bool oldValue, bool newValue)
    {
    }
}

[AttachedDependencyProperty<object, TreeView>(""SelectedItem"", BindsTwoWayByDefault = true)]
public static partial class TreeViewExtensions
{
    // Optional
    static partial void OnSelectedItemChanged(TreeView sender, object? oldValue, object? newValue)
    {
    }
}");
    }

    [TestMethod]
    public Task GeneratesCorrectlyIfHaveMultipleClassDeclarations()
    {
        return this.CheckSourceAsync(@"
using DependencyPropertyGenerator;
using System.Windows;

#nullable enable

namespace H.Generators.IntegrationTests;

[DependencyProperty<bool>(""IsSpinning"")]
public partial class MainWindow : Window
{
    // Optional
    partial void OnIsSpinningChanged(bool oldValue, bool newValue)
    {
    }
}

[DependencyProperty<bool>(""IsSpinning2"")]
public partial class MainWindow
{
    // Optional
    partial void OnIsSpinning2Changed(bool oldValue, bool newValue)
    {
    }
}");
    }

    [TestMethod]
    public Task GeneratesEnumCorrectly()
    {
        return this.CheckSourceAsync(@"
using DependencyPropertyGenerator;
using System.Windows.Controls;

#nullable enable

namespace H.Generators.IntegrationTests;

public enum Mode
{
    Mode1,
    Mode2,
}

[AttachedDependencyProperty<Mode, TreeView>(""Mode"")]
public static partial class TreeViewExtensions
{
    static partial void OnModeChanged(TreeView sender, Mode oldValue, Mode newValue)
    {
    }
}");
    }

    [TestMethod]
    public Task GeneratesRoutedEventCorrectly()
    {
        return this.CheckSourceAsync(@"
using DependencyPropertyGenerator;
using System.Windows;

#nullable enable

namespace H.Generators.IntegrationTests;

[RoutedEvent(""TrayLeftMouseDown"", RoutedEventStrategy.Bubble)]
public partial class MainWindow : Window
{
}");
    }

    [TestMethod]
    public Task GeneratesAttachedRoutedEventCorrectly()
    {
        return this.CheckSourceAsync(@"
using DependencyPropertyGenerator;
using System.Windows;

#nullable enable

namespace H.Generators.IntegrationTests;

[RoutedEvent(""TrayLeftMouseDown"", RoutedEventStrategy.Bubble, IsAttached = true)]
public partial class MainWindow : Window
{
}");
    }
}