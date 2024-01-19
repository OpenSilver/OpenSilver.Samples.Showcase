namespace OpenSilver.Samples.Showcase

open OpenSilver
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

type ExecuteJavaScript_Demo() as this =
    inherit ExecuteJavaScript_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.SendJavaScriptMessage(sender: obj, e: RoutedEventArgs) =
        Interop.ExecuteJavaScript("alert($0);", this.TextBoxInput.Text) |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.fs")
        ])
