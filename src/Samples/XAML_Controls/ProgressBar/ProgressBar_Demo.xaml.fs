namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("progress", "indicator", "loading", "status", "UI")>]
type ProgressBar_Demo() as this =
    inherit ProgressBar_DemoXaml()

    do
        this.InitializeComponent()
