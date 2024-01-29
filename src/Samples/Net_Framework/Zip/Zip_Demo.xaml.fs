namespace OpenSilver.Samples.Showcase

open OpenSilver.Extensions.FileSystem
open Ionic.Zip
open System.Windows
open System.Windows.Controls

type Zip_Demo() as this =
    inherit Zip_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonGenerateZip_Click (sender: obj, e: RoutedEventArgs) =
        async {
            use zipFile = new ZipFile()
            do! zipFile.AddFile("SampleText.txt", "Hello World!")
            let! jsBlob = zipFile.SaveToJavaScriptBlob()
            do! FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip")
        } |> Async.Start
