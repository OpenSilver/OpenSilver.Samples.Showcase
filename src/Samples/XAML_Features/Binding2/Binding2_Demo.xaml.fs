namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Collections.ObjectModel
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

type Binding2_Demo() as this =
    inherit Binding2_DemoXaml()

    do
        this.InitializeComponent()
        this.Title.Content <- "Binding (2 of 3)"

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Binding2_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/Binding2_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Binding2_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/Binding2_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "KilometersToMilesConverter.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/KilometersToMilesConverter.cs");
            ViewSourceButtonInfo(TabHeader = "Binding2_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/Binding2_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "KilometersToMilesConverter.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/KilometersToMilesConverter.vb")
            ViewSourceButtonInfo(TabHeader = "Binding2_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/Binding2_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "KilometersToMilesConverter.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding2/KilometersToMilesConverter.fs")
        ])
