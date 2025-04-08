namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("data", "MVVM", "binding", "UI")>]
type Binding2_Demo() as this =
    inherit Binding2_DemoXaml()

    do
        this.InitializeComponent()
        this.Title.Content <- "Binding (2 of 3)"
