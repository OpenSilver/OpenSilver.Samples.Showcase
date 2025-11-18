namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("container")>]
type HeaderedContentControl_Demo() as this =
    inherit HeaderedContentControl_DemoXaml()
    do
        this.InitializeComponent()
