namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("text", "display", "label", "content", "UI")>]
type TextBlock_Demo() as this =
    inherit TextBlock_DemoXaml()
    
    do
        this.InitializeComponent()
