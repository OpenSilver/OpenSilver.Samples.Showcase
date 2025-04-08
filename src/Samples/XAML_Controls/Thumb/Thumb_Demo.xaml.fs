namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("drag", "thumb", "slider", "scroll", "UI")>]
type Thumb_Demo() as this =
    inherit Thumb_DemoXaml()
    
    do
        this.InitializeComponent()
