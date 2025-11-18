namespace OpenSilver.Showcase

open System.Windows
open OpenSilver.Extensions.FileSystem
open OpenSilver.Showcase.Search

[<SearchKeywords("file", "save")>]
type File_Save_Demo() as this =
    inherit File_Save_DemoXaml()

    do
        this.InitializeComponent()

    member this.ButtonSave_Click (sender : obj, e : RoutedEventArgs) =
        async {
            do! FileSaver.SaveTextToFile("Hello World!", "MyTestFile.txt")
        } |> Async.Start
