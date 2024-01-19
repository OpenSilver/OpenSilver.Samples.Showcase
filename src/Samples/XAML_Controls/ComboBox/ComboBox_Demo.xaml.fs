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

type ComboBox_Demo() as this =
    inherit ComboBox_DemoXaml()
    
    do
        this.InitializeComponent()
        this.ComboBox1.ItemsSource <- Planet.GetListOfPlanets()

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "ComboBox_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "ComboBox_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ComboBox_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ComboBox_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "DefaultComboBoxStyle.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/Styles/DefaultComboBoxStyle.xaml");
            ViewSourceButtonInfo(TabHeader = "Planet.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.cs");
            ViewSourceButtonInfo(TabHeader = "Planet.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.vb");
            ViewSourceButtonInfo(TabHeader = "Planet.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.fs")
        ])
