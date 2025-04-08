namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("console", "output", "debugging", "logging")>]
type Console_Demo() as this =
    inherit Console_DemoXaml()

    do
        this.InitializeComponent()

