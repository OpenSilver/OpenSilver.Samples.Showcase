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

type Linq_Demo() as this =
    inherit Linq_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.ButtonToDemonstrateLinq_Click(sender : obj, e : RoutedEventArgs) =
        let planets = Planet.GetListOfPlanets()

        let result = 
            [ for p in planets do
                if p.Radius > 7000 then
                    yield p.Name 
            ] |> List.sort

        MessageBox.Show(sprintf "List of planets that have a radius greater than 7000km sorted alphabetically: %s" (String.Join(", ", result))) |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Linq_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Linq/Linq_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Linq_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Linq/Linq_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Planet.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.cs");
            ViewSourceButtonInfo(TabHeader = "Linq_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Linq/Linq_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Planet.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.vb");
            ViewSourceButtonInfo(TabHeader = "Linq_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Linq/Linq_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "Planet.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.fs")
        ])
