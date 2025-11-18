namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("URI", "navigation", "mapping", "routing", "UI")>]
type UriMapper_Demo() as this =
    inherit UriMapper_DemoXaml()

    do
        this.InitializeComponent()

