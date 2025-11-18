namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("media", "image", "picture", "graphics", "display")>]
type Image_Demo() as this =
    inherit Image_DemoXaml()

    do
        this.InitializeComponent()
