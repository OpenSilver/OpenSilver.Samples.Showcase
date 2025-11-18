namespace OpenSilver.Showcase

open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("items", "expander", "list")>]
type public Accordion_Demo() as this =
    inherit Accordion_DemoXaml()

    do
        this.InitializeComponent()
