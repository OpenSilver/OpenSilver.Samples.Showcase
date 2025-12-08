namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("scroll", "viewer", "content", "control", "container", "layout")>]
type ScrollViewer_Demo() as this =
    inherit ScrollViewer_DemoXaml()
    
    do
        this.InitializeComponent()
