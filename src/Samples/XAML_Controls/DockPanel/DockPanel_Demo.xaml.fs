namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("layout", "dock", "panel", "container", "arrangement")>]
type DockPanel_Demo() as this =
    inherit DockPanel_DemoXaml()

    do
        this.InitializeComponent()
