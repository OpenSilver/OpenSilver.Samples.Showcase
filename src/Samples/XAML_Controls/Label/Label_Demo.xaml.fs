namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Media
open System.Windows.Navigation
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

type Label_Demo() as this =
    inherit Label_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ButtonViewMore_Click(sender : obj, e : RoutedEventArgs) =
        //this.ChildWindowHelper.ShowChildWindow(Button_Demo_More())
        ()

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You clicked the button!") |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Label_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Label/Label_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Label_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Label/Label_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Label_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Label/Label_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Label_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Label/Label_Demo.xaml.fs")
        ])
