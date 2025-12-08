namespace OpenSilver.Showcase

open  System.IO
open  System.Text
open  System.Windows
open  OpenSilver.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("file", "dialog", "filesystem", "browse")>]
type OpenFileDialog_Demo() as this =
    inherit OpenFileDialog_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonOpenFile_Click(sender: obj, e: RoutedEventArgs) =
        async {
            let openFileDialog1 = new OpenFileDialog(Filter = "Text files (*.txt)|*.txt")
            let! result = openFileDialog1.ShowDialogAsync() |> Async.AwaitTask
            match result.HasValue with
            | true when result.Value -> 
                try
                    let file = openFileDialog1.File
                    this.FileNameTextBlock.Text <- file.Name
                    use reader = new StreamReader(file.OpenRead(), Encoding.UTF8)
                    MessageBox.Show(reader.ReadToEnd()) |> ignore
                with
                | ex -> () // Fail silently
            | _ -> ()
        } |> Async.Start
