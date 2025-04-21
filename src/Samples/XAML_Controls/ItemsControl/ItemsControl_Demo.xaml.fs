namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("list", "collection")>]
type ItemsControl_Demo() as this =
    inherit ItemsControl_DemoXaml()
    
    do
        this.InitializeComponent()
