namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("list", "collection")>]
type ItemsControl_Demo() as this =
    inherit ItemsControl_DemoXaml()
    
    do
        this.InitializeComponent()
