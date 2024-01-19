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

type Binding1_Demo() as this =
    inherit Binding1_DemoXaml()

    do
        this.InitializeComponent()

        let listOfPlanets = Planet.GetListOfPlanets()
        this.ItemsControl1.ItemsSource <- listOfPlanets
        this.ContentControl1.Content <- listOfPlanets.[0]

    member private this.ButtonPlanet_Click(sender : obj, e : RoutedEventArgs) =
        let planet = (sender :?> Button).DataContext
        this.ContentControl1.Content <- planet

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Binding1_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding1/Binding1_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Binding1_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding1/Binding1_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Planet.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.cs");
            ViewSourceButtonInfo(TabHeader = "Binding1_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding1/Binding1_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Planet.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.vb");
            ViewSourceButtonInfo(TabHeader = "Binding1_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding1/Binding1_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "Planet.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.fs")
        ])
