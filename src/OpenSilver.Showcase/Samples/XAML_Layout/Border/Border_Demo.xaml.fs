namespace OpenSilver.Showcase

open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("layout", "border", "frame", "container", "UI")>]
type Border_Demo() as this =
    inherit Border_DemoXaml()
    
    do
        this.InitializeComponent()
