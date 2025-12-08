namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "canvas", "absolute positioning", "drawing", "graphics")>]
type Canvas_Demo() as this =
    inherit Canvas_DemoXaml()
    
    do
        this.InitializeComponent()
