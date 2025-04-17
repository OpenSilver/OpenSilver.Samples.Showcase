namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("progressive", "web", "application", "desktop", "offline")>]
type PWA_Demo() as this =
    inherit PWA_DemoXaml()
    do this.InitializeComponent()
