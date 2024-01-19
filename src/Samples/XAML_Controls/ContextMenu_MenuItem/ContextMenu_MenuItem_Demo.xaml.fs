namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

type ContextMenu_MenuItem_Demo() as this =
    inherit ContextMenu_MenuItem_DemoXaml()

    do
        this.InitializeComponent()

    member private this.MenuItem1_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 1") |> ignore

    member private this.MenuItem2_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 2") |> ignore

    member private this.MenuItem3_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 3") |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "ContextMenu_MenuItem_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ContextMenu_MenuItem/ContextMenu_MenuItem_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "ContextMenu_MenuItem_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ContextMenu_MenuItem/ContextMenu_MenuItem_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ContextMenu_MenuItem_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ContextMenu_MenuItem/ContextMenu_MenuItem_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ContextMenu_MenuItem_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ContextMenu_MenuItem/ContextMenu_MenuItem_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "DefaultMenuItemStyle.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ContextMenu_MenuItem/Styles/DefaultMenuItemStyle.xaml")
        ])
