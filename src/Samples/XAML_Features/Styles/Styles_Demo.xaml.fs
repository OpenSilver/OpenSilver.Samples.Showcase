namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("styling", "XAML", "themes", "customization", "UI")>]
type Styles_Demo() as this =
    inherit Styles_DemoXaml()

    do
        this.InitializeComponent()
