namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("tree", "hierarchy", "nodes", "expand", "UI")>]
type TreeView_Demo() as this =
    inherit TreeView_DemoXaml()

    do
        this.InitializeComponent()
