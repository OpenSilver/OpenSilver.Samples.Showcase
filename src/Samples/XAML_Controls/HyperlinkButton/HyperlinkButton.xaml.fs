namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("hyperlink", "text", "navigation", "control", "button", "web")>]
type HyperlinkButton_Demo() as this =
    inherit HyperlinkButton_DemoXaml()
    
    do
        this.InitializeComponent()
