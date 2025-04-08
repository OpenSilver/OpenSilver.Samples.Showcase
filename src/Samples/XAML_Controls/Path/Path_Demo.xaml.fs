namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("graphics", "vector", "drawing", "shapes", "UI")>]
type Path_Demo() as this =
    inherit Path_DemoXaml()
    
    do
        this.InitializeComponent()
