namespace OpenSilver.Samples.Showcase

open PreviewOnWinRT
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

type NonModalChildWindow_Demo() as this =
    inherit NonModalChildWindow_DemoXaml()

    let mutable _n = 0

    do
        this.InitializeComponent()

    member private this.ButtonTestChildWindow_Modal_Click(sender : obj, e : RoutedEventArgs) =
        let childWindow = SmallChildWindow()
        childWindow.Title <- "ChildWindow (Modal)" + string _n
        _n <- _n + 1
#if !OPENSILVER
        childWindow.IsModal <- true
#endif
        childWindow.Show()

    member private this. ButtonTestChildWindow_NonModal_Click(sender : obj, e : RoutedEventArgs) =
        let childWindow = SmallChildWindow()
        childWindow.Title <- "ChildWindow (Non-modal)" + string _n
        _n <- _n + 1
#if !OPENSILVER
        childWindow.IsModal <- false
#endif
        childWindow.Show()

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "NonModalChildWindow_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "NonModalChildWindow_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "NonModalChildWindow_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "NonModalChildWindow_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "SmallChildWindow.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml");
            ViewSourceButtonInfo(TabHeader = "SmallChildWindow.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "SmallChildWindow.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "SmallChildWindow.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "DefaultChildWindowStyle.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/Styles/DefaultChildWindowStyle.xaml")
        ])
