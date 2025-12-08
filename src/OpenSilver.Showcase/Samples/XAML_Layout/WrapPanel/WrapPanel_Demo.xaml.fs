namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "wrap", "panel", "container", "arrangement", "orientation", "items")>]
type WrapPanel_Demo() as this =
    inherit WrapPanel_DemoXaml()
    
    do
        this.InitializeComponent()

