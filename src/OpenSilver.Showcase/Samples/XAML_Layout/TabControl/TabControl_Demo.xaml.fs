namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("navigation", "tab", "interface", "section", "control")>]
type TabControl_Demo() as this =
    inherit TabControl_DemoXaml()
    
    do
        this.InitializeComponent()
