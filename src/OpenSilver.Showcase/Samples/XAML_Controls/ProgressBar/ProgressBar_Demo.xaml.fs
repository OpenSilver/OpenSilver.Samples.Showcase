namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("progress", "indicator", "loading", "status", "UI")>]
type ProgressBar_Demo() as this =
    inherit ProgressBar_DemoXaml()

    do
        this.InitializeComponent()
