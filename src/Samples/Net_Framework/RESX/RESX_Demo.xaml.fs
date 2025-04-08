namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Resources
open System.Reflection
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("resources", "localization", "RESX", "translation")>]
type RESX_Demo() as this =
    inherit RESX_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonReadResource_Click(sender: obj, e: RoutedEventArgs) =
        let resourceManager = ResourceManager("OpenSilver.Samples.Showcase.Other.SampleResourceFileFS", Assembly.GetExecutingAssembly())
        MessageBox.Show(resourceManager.GetString("InfoMessage")) |> ignore
