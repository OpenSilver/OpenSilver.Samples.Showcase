namespace OpenSilver.Samples.Showcase

open OpenSilver
open System.Windows
open System.Windows.Controls

type ExecuteJavaScript_Demo() as this =
    inherit ExecuteJavaScript_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.SendJavaScriptMessage(sender: obj, e: RoutedEventArgs) =
        Interop.ExecuteJavaScript("alert($0);", this.TextBoxInput.Text) |> ignore
