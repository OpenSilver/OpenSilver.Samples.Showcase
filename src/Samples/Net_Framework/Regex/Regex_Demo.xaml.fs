namespace OpenSilver.Samples.Showcase

open System.Text.RegularExpressions
open System.Windows
open System.Windows.Controls

type Regex_Demo() as this =
    inherit Regex_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonReplaceDates_Click(sender: obj, e: RoutedEventArgs) =
        this.TextBlockOutputOfRegexReplaceDemo.Text <- Regex.Replace(this.TextBoxRegexReplaceDemo.Text, @"(\d{2})/(\d{2})/(\d{2,4})", "$3-$2-$1")
