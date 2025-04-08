namespace OpenSilver.Samples.Showcase

open System.Windows
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("clipboard", "copy", "paste", "text", "interaction")>]
type Clipboard_SetText_Demo() as this =
    inherit Clipboard_SetText_DemoXaml()

    do
        this.InitializeComponent()


    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        async {
            do! Clipboard.SetTextAsync(this.ClipboardTextBox.Text) |> Async.AwaitTask
        } |> Async.Start
        MessageBox.Show("Text copied!") |> ignore
