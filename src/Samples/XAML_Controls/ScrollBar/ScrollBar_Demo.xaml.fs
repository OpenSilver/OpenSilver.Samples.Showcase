namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("scroll", "bar", "navigation", "UI", "container")>]
type ScrollBar_Demo() as this =
    inherit ScrollBar_DemoXaml()

    do
        this.InitializeComponent()
