namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("clipping", "bounding box", "UI")>]
type ClipToBounds_Demo() as this =
    inherit ClipToBounds_DemoXaml()
    
    do
        this.InitializeComponent()

