namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("layout", "grid", "rows", "columns", "container")>]
type Grid_Demo() as this =
    inherit Grid_DemoXaml()

    do
        this.InitializeComponent()
