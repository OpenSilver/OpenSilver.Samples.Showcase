namespace OpenSilver.Samples.Showcase

open System.Windows
open Ionic.Zip
open OpenSilver.Extensions.FileSystem

type JSZip_Demo() as this =
    inherit JSZip_DemoXaml()
    do this.InitializeComponent()
    
    member private this.ButtonGenerateZip_Click(sender: obj, e: RoutedEventArgs) : unit =
        let task = task {
            let zipFile = new ZipFile()
            do! zipFile.AddFile("SampleText.txt", "Hello World!")
            let! jsBlob = zipFile.SaveToJavaScriptBlob()
            if jsBlob <> null then
                do! FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip")
        }
        task.Start() |> ignore
