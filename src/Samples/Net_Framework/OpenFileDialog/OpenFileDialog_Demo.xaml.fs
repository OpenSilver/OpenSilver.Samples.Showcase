namespace OpenSilver.Samples.Showcase

open  System
open  System.Collections.Generic
open  System.Diagnostics
open  System.IO
open  System.Linq
open  System.Runtime.InteropServices.ComTypes
open  System.Security
open  System.Text
open  System.Windows
open  System.Windows.Controls
open  System.Windows.Controls.Primitives
open  System.Windows.Data
open  System.Windows.Input
open  System.Windows.Media
open  System.Windows.Navigation
open  OpenSilver.Controls

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
