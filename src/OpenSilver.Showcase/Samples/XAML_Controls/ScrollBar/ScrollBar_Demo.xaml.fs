namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("scroll", "bar", "navigation", "UI", "container")>]
type ScrollBar_Demo() as this =
    inherit ScrollBar_DemoXaml()

    do
        this.InitializeComponent()
