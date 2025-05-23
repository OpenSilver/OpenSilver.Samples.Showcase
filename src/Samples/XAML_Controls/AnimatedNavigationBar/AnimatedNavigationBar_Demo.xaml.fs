namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("navigation", "bar", "navbar", "animation", "tab", "menu", "navigation bar")>]
type AnimatedNavigationBar_Demo() as this =
    inherit AnimatedNavigationBar_DemoXaml()
    
    do this.InitializeComponent()
