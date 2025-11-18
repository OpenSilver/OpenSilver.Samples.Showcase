namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "grid", "rows", "columns", "container", "panel", "contentcontrol", "contentpresenter")>]
type UniformGrid_Demo() as this =
    inherit UniformGrid_DemoXaml()
    do this.InitializeComponent()
