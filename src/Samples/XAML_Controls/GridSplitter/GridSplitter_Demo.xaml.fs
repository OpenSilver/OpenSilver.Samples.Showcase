namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("layout", "grid", "splitter", "resizable", "divider", "panel")>]
type GridSplitter_Demo() as this =
    inherit GridSplitter_DemoXaml()
    
    do
        this.InitializeComponent()
