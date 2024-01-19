namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows.Browser
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

type HtmlPage_Demo() as this =
    inherit HtmlPage_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonGetCurrentURL_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("The current URL is: " + HtmlPage.Document.DocumentUri.OriginalString) |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "HtmlPage_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/HtmlPage/HtmlPage_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "HtmlPage_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/HtmlPage/HtmlPage_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "HtmlPage_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/HtmlPage/HtmlPage_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "HtmlPage_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/HtmlPage/HtmlPage_Demo.xaml.fs")
        ])
