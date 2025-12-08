namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "updown", "spinner", "counter", "control", "buttonspinner")>]
type public DomainUpDown_Demo() as this =
    inherit DomainUpDown_DemoXaml()

    do
        this.InitializeComponent()
