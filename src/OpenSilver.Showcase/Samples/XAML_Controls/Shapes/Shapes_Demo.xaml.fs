namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("graphics", "shapes", "drawing", "vector", "UI")>]
type Shapes_Demo() as this =
    inherit Shapes_DemoXaml()
    
    do
        this.InitializeComponent()
