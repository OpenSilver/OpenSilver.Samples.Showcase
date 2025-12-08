namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("layout", "measure", "arrange", "positioning", "UI")>]
type MeasureArrange_Demo() as this =
    inherit MeasureArrange_DemoXaml()

    do
        this.InitializeComponent()
