namespace OpenSilver.Showcase

open System.IO
open System.IO.Compression
open System.Windows
open OpenSilver.Controls

type Zip_Demo() as this =
    inherit Zip_DemoXaml()
    do this.InitializeComponent()

    member private this.ButtonGenerateZip_Click(sender: obj, e: RoutedEventArgs) : unit =
        let task = task {
            use memoryStream = new MemoryStream()
            do
                use archive = new ZipArchive(memoryStream, ZipArchiveMode.Create)
                let zipEntry = archive.CreateEntry("SampleText.txt", CompressionLevel.Optimal)
                use entryStream = zipEntry.Open()
                use writer = new StreamWriter(entryStream)
                writer.Write("Hello World!")

            let dialog = SaveFileDialog()
            dialog.DefaultExt <- ".zip"
            dialog.Filter <- "Zip files (*.zip)|*.zip|All files (*.*)|*.*"
            dialog.DefaultFileName <- "MyTestFile"

            let! result = dialog.ShowDialogAsync()
            if result.HasValue && result.Value then
                let data = memoryStream.ToArray()
                use! saveFileStream = dialog.OpenFileAsync()
                do! saveFileStream.WriteAsync(data, 0, data.Length)
                do! saveFileStream.FlushAsync()
        }
        task.Start() |> ignore
