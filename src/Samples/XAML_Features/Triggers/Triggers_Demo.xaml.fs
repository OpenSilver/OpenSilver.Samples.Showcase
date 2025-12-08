namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("triggers", "styling", "UI", "behavior", "events")>]
type Triggers_Demo() as this =
    inherit Triggers_DemoXaml()

    do
        this.InitializeComponent()
