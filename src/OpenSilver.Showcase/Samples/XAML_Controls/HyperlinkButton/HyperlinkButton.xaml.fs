namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("hyperlink", "text", "navigation", "control", "button", "web")>]
type HyperlinkButton_Demo() as this =
    inherit HyperlinkButton_DemoXaml()
    
    do
        this.InitializeComponent()
