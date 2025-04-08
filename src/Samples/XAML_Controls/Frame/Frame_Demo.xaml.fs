namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("navigation", "frame", "content", "view", "container")>]
type Frame_Demo() as this =
    inherit Frame_DemoXaml()

    do
        this.InitializeComponent()
