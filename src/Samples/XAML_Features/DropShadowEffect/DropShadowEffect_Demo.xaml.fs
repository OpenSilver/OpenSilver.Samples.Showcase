namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("graphics", "shadow", "effect", "blur", "UI")>]
type DropShadowEffect_Demo() as this =
    inherit DropShadowEffect_DemoXaml()
    
    do
        this.InitializeComponent()
