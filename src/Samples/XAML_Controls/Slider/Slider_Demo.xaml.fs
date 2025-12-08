namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "slider", "range", "selection", "control")>]
type Slider_Demo() as this =
    inherit Slider_DemoXaml()
    
    do
        this.InitializeComponent()
