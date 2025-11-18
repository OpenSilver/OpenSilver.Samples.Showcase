namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("tooltip", "hover", "info", "help", "UI")>]
type ToolTip_Demo() as this =
    inherit ToolTip_DemoXaml()
    
    do
        this.InitializeComponent()

