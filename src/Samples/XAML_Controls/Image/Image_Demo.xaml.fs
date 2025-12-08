namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("media", "image", "picture", "graphics", "display")>]
type Image_Demo() as this =
    inherit Image_DemoXaml()

    do
        this.InitializeComponent()
