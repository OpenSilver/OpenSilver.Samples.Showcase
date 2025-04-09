namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("container")>]
type HeaderedContentControl_Demo() as this =
    inherit HeaderedContentControl_DemoXaml()
    do
        this.InitializeComponent()
