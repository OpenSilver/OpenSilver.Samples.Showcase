namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Input
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

type Keyboard_Demo() as this =
    inherit Keyboard_DemoXaml()

    do
        this.InitializeComponent()

#if SLMIGRATION
    member private this.TextBoxInput_KeyDown(sender : obj, e : KeyEventArgs) =
        if e.Key = Key.Enter then
#else
    member private this.TextBoxInput_KeyDown(sender : obj, e : KeyRoutedEventArgs) =
        if e.Key = Windows.System.VirtualKey.Enter then
#endif
            MessageBox.Show(sprintf "You pressed Enter!%s%sThis is the text that you entered: %s" Environment.NewLine Environment.NewLine this.TextBoxInput.Text) |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Keyboard_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Keyboard/Keyboard_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Keyboard_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Keyboard/Keyboard_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Keyboard_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Keyboard/Keyboard_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Keyboard_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Keyboard/Keyboard_Demo.xaml.fs")
        ])
