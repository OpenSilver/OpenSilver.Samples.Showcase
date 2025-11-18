namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("scaling", "resize", "zoom", "layout", "UI")>]
type Viewbox_Demo() as this =
    inherit Viewbox_DemoXaml()
    
    do
        this.InitializeComponent()

