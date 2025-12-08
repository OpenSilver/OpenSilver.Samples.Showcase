namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("brush", "image", "fill", "background", "UI")>]
type ImageBrush_Demo() as this =
    inherit ImageBrush_DemoXaml()

    do
        this.InitializeComponent()
