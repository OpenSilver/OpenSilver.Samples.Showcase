namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("performance", "progressive loading", "lazy loading", "incremental load", "UI")>]
type ProgressiveLoading_Demo() as this =
    inherit ProgressiveLoading_DemoXaml()

    do
        this.InitializeComponent()
