namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("items", "expander", "list")>]
type public Accordion_Demo() as this =
    inherit Accordion_DemoXaml()

    do
        this.InitializeComponent()
