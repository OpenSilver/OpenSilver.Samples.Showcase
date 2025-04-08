namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Browser
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("HTML", "browser", "web", "host")>]
type HtmlPage_Demo() as this =
    inherit HtmlPage_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonGetCurrentURL_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("The current URL is: " + HtmlPage.Document.DocumentUri.OriginalString) |> ignore
