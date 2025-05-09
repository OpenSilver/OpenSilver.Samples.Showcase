namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("layout", "stack", "vertical", "horizontal", "container")>]
type StackPanel_Demo() as this =
    inherit StackPanel_DemoXaml()
    
    do
        this.InitializeComponent()
