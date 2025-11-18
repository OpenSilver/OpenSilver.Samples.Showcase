namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("text", "label", "description", "content", "UI")>]
type Label_Demo() as this =
    inherit Label_DemoXaml()
    
    do
        this.InitializeComponent()
