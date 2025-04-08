namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("brush", "image", "fill", "background", "UI")>]
type ImageBrush_Demo() as this =
    inherit ImageBrush_DemoXaml()

    do
        this.InitializeComponent()
