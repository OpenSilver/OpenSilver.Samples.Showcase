namespace OpenSilver.Showcase

open OpenSilver
open System.Windows
open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("JavaScript", "interop", "browser", "script")>]
type ExecuteJavaScript_Demo() as this =
    inherit ExecuteJavaScript_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.SendJavaScriptMessage(sender: obj, e: RoutedEventArgs) =
        Interop.ExecuteJavaScript("alert($0);", this.TextBoxInput.Text) |> ignore
