namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "toggle", "option", "selection", "form")>]
type RadioButton_Demo() as this =
    inherit RadioButton_DemoXaml()
    
    do
        this.InitializeComponent()
