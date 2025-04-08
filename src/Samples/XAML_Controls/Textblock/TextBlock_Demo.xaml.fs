namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("text", "display", "label", "content", "UI")>]
type TextBlock_Demo() as this =
    inherit TextBlock_DemoXaml()
    
    do
        this.InitializeComponent()
