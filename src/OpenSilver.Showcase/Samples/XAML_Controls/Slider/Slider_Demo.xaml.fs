namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("input", "slider", "range", "selection", "control")>]
type Slider_Demo() as this =
    inherit Slider_DemoXaml()
    
    do
        this.InitializeComponent()
