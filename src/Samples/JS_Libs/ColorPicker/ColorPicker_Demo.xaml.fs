namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

[<OpenSilver.Samples.Showcase.Search.SearchKeywords("color", "picker", "palette", "interop", "javascript", "alwan")>]
type ColorPicker_Demo() as this =
    inherit ColorPicker_DemoXaml()
    do this.InitializeComponent()
