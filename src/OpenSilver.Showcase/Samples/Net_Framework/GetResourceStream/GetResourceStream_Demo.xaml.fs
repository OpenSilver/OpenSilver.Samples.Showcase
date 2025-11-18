namespace OpenSilver.Showcase

open System
open System.IO
open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("resource", "stream", "file", "embedded resources", "load", "content")>]
type GetResourceStream_Demo() as this =
    inherit GetResourceStream_DemoXaml()

    do
        this.InitializeComponent()

    member this.ViewFile_Click(sender: obj, e: RoutedEventArgs) =
        let uri = Uri("/OpenSilver.Showcase;component/Other/SampleText.txt", UriKind.Relative)
        let content = this.RetrieveFileContent(uri)
        MessageBox.Show(sprintf "URI %O contains:\n%s" uri content) |> ignore

    member this.RetrieveFileContent(uri: Uri) : string =
        let resourceStream = Application.GetResourceStream(uri).Result
        use currentReader = new StreamReader(resourceStream.Stream)
        currentReader.ReadToEnd()
