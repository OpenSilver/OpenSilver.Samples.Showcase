namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "stack", "vertical", "horizontal", "container")>]
type StackPanel_Demo() as this =
    inherit StackPanel_DemoXaml()
    
    do
        this.InitializeComponent()
