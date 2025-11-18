namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("input", "collapse", "expand", "panel", "container", "control", "headeredcontentcontrol")>]
type Expander_Demo() as this =
    inherit Expander_DemoXaml()
    
    do
        this.InitializeComponent()
