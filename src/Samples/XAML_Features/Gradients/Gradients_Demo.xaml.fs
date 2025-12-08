namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("graphics", "linear", "radial", "gradient", "brush", "color", "fill")>]
type Gradients_Demo() as this =
    inherit Gradients_DemoXaml()
    
    do
        this.InitializeComponent()
