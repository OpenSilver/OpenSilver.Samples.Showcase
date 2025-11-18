namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "grid", "splitter", "resizable", "divider", "panel")>]
type GridSplitter_Demo() as this =
    inherit GridSplitter_DemoXaml()
    
    do
        this.InitializeComponent()
