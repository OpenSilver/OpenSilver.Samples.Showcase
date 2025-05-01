namespace OpenSilver.Samples.Showcase

open System.Windows
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("HTML", "interop", "js", "numeric", "colorpicker")>]
type Interop_HtmlPresenter_Demo() as this =
    inherit Interop_HtmlPresenter_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonClickHere_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("The value is: " + this.NumericTextBox1.Value.ToString()) |> ignore
