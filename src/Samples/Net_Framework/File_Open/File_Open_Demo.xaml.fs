namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type File_Open_Demo() as this =
    inherit File_Open_DemoXaml()

    do
        this.InitializeComponent()

    member this.OnFileOpened(sender : obj, e : OpenSilver.Extensions.FileOpenDialog.FileOpenedEventArgs) =
        MessageBox.Show(e.Text) |> ignore
