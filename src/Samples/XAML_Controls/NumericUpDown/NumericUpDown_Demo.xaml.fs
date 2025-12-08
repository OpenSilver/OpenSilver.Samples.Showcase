namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "numeric", "updown", "spinner", "counter", "control", "buttonspinner")>]
type NumericUpDown_Demo() as this =
    inherit NumericUpDown_DemoXaml()
    
    do
        this.InitializeComponent()
