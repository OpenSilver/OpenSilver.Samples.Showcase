namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("graphics", "blur", "effect", "filter", "UI", "visual", "image")>]
type BlurEffect_Demo() as this =
    inherit BlurEffect_DemoXaml()
    
    do
        this.InitializeComponent()
