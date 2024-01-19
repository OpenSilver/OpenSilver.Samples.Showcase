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

type WebView_Demo() as this =
    inherit WebView_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ButtonNavigate_Click(sender : obj, e : RoutedEventArgs) =
        this.WebView1.Navigate(new Uri(this.TextBoxWithURL.Text))

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "WebBrowser_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase/Samples/Interop_Samples/WebView/WebBrowser_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "WebBrowser_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase/Samples/Interop_Samples/WebView/WebBrowser_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "WebBrowser_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase/Samples/Interop_Samples/WebView/WebBrowser_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "WebBrowser_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase/Samples/Interop_Samples/WebView/WebBrowser_Demo.xaml.fs")
        ])
