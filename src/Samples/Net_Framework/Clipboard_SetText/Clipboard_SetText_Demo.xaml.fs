namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows
#if SLMIGRATION
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

type Clipboard_SetText_Demo() as this =
    inherit Clipboard_SetText_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Clipboard_SetText_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Clipboard_SetText/Clipboard_SetText_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Clipboard_SetText_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Clipboard_SetText/Clipboard_SetText_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Clipboard_SetText_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Clipboard_SetText/Clipboard_SetText_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Clipboard_SetText_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Clipboard_SetText/Clipboard_SetText_Demo.xaml.fs")
        ])

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        async {
            do! Clipboard.SetTextAsync(this.ClipboardTextBox.Text) |> Async.AwaitTask
        } |> Async.Start
        MessageBox.Show("Text copied!") |> ignore
