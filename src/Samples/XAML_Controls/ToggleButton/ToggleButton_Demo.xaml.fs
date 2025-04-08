namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "toggle", "switch", "boolean", "button", "control")>]
type ToggleButton_Demo() as this =
    inherit ToggleButton_DemoXaml()
    
    do
        this.InitializeComponent()
