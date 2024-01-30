namespace OpenSilver.Samples.Showcase

open System
open System.IO
open System.Windows
open System.Windows.Controls

type GetRessourceStream_Demo() as this =
    inherit GetRessourceStream_DemoXaml()
    
    let mutable currentUri : Uri = Unchecked.defaultof<_>

    do
        base.InitializeComponent()

    member private this.ViewFile_Click (sender : obj, e : RoutedEventArgs) =
        currentUri <- Uri("ms-appx:/Other/SampleText.txt")
        let getFileContent = this.RetrieveFileContent(currentUri)

        async {
            let! currentFileContent = getFileContent
            MessageBox.Show("Contains: " + currentFileContent) |> ignore
        } |> Async.Start

    member private this.RetrieveFileContent (uri : Uri) =
        async {
            let! resourceStream = Application.GetResourceStream uri |> Async.AwaitTask
            use currentReader = new StreamReader(resourceStream.Stream)

            MessageBox.Show("Opening : " + uri.AbsoluteUri) |> ignore
            let! result = currentReader.ReadToEndAsync() |> Async.AwaitTask
            return result
        }
