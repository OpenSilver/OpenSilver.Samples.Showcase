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

type GetDiv_Demo() as this =
    inherit GetDiv_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ButtonSetCSS_Click(sender: obj, e: RoutedEventArgs) =
        let div = Interop.GetDiv(this)

        Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div) |> ignore

        // Note: refer to the documentation at: http://opensilver.net/links/how-to-call-javascript.aspx

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "GetDiv_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ExecuteJavaScript_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.fs")
        ])