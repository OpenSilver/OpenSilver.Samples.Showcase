namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Text.RegularExpressions
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

type Regex_Demo() as this =
    inherit Regex_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonReplaceDates_Click(sender: obj, e: RoutedEventArgs) =
        this.TextBlockOutputOfRegexReplaceDemo.Text <- Regex.Replace(this.TextBoxRegexReplaceDemo.Text, @"(\d{2})/(\d{2})/(\d{2,4})", "$3-$2-$1")

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Regex_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Regex/Regex_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Regex_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Regex/Regex_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Regex_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Regex/Regex_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Regex_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Regex/Regex_Demo.xaml.fs")
        ])
