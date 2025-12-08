namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("RTL", "right to left", "flow", "direction", "layout", "arabic", "hebrew", "localization", "internationalization")>]
type FlowDirection_Demo() as this =
    inherit FlowDirection_DemoXaml()

    do
        this.InitializeComponent()
