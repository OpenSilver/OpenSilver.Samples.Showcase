namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("RIA", "services", "data", "client", "server", "network")>]
type RIAServices_Demo() as this =
    inherit RIAServices_DemoXaml()
    
    do
        this.InitializeComponent()
        