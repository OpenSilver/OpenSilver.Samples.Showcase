namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("graphics", "vector", "drawing", "shapes", "UI")>]
type Path_Demo() as this =
    inherit Path_DemoXaml()
    
    do
        this.InitializeComponent()
