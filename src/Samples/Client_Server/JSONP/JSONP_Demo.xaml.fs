namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("JSONP", "cross-domain", "JavaScript", "web", "API")>]
type JSONP_Demo() as this =
    inherit JSONP_DemoXaml()
    
    do
        this.InitializeComponent()
