namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("progressive", "web", "application", "desktop", "offline")>]
type PWA_Demo() as this =
    inherit PWA_DemoXaml()
    do this.InitializeComponent()
