namespace OpenSilver.Samples.Showcase

open System
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("hyperlink", "text", "navigation", "control", "button", "web")>]
type HyperlinkButton_Demo() as this =
    inherit HyperlinkButton_DemoXaml()
    
    do
        this.InitializeComponent()

#if OPENSILVER
        this.HyperlinkButtonDemo.NavigateUri <- Uri("http://www.opensilver.net")
#endif
