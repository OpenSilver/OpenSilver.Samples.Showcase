namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("performance", "AOT", "ahead-of-time", "compilation", "optimization")>]
type AOT_Demo() as this =
    inherit AOT_DemoXaml()

    do
        this.InitializeComponent()
