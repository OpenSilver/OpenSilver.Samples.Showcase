namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("transform", "rotation", "scaling", "skew", "matrix", "translate", "composite", "translation", "UI")>]
type RenderTransforms_Demo() as this =
    inherit RenderTransforms_DemoXaml()
    
    do
        this.InitializeComponent()
