namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("graphics", "linear", "radial", "gradient", "brush", "color", "fill")>]
type Gradients_Demo() as this =
    inherit Gradients_DemoXaml()
    
    do
        this.InitializeComponent()
