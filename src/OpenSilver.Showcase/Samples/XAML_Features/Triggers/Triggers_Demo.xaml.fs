namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("triggers", "styling", "UI", "behavior", "events")>]
type Triggers_Demo() as this =
    inherit Triggers_DemoXaml()

    do
        this.InitializeComponent()
