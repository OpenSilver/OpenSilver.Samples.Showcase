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

type CheckBox_Demo() as this =
    inherit CheckBox_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.CheckBox_Checked(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You checked me.") |> ignore

    member this.CheckBox_Unchecked(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You unchecked me.") |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "CheckBox_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "CheckBox_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "CheckBox_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "CheckBox_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml.fs")
        ])
