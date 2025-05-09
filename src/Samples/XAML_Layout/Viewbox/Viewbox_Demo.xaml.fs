namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("scaling", "resize", "zoom", "layout", "UI")>]
type Viewbox_Demo() as this =
    inherit Viewbox_DemoXaml()
    
    do
        this.InitializeComponent()

