namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("tooltip", "hover", "info", "help", "UI")>]
type ToolTip_Demo() as this =
    inherit ToolTip_DemoXaml()
    
    do
        this.InitializeComponent()

