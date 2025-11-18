namespace OpenSilver.Showcase

open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "dock", "panel", "container", "arrangement")>]
type DockPanel_Demo() as this =
    inherit DockPanel_DemoXaml()

    do
        this.InitializeComponent()
