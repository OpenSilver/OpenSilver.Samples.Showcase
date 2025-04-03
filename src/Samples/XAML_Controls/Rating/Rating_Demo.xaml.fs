namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "stars", "control")>]
type public Rating_Demo() as this =
    inherit Rating_DemoXaml()

    do
        this.InitializeComponent()
