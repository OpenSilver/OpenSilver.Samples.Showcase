namespace OpenSilver.Samples.Showcase

open System.Windows
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("file", "open", "dialog", "filesystem", "load")>]
type File_Open_Demo() as this =
    inherit File_Open_DemoXaml()

    do
        this.InitializeComponent()

    member this.OnFileOpened(sender : obj, e : OpenSilver.Extensions.FileOpenDialog.FileOpenedEventArgs) =
        MessageBox.Show(e.Text) |> ignore
