namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("input", "toggle", "option", "selection", "form")>]
type RadioButton_Demo() as this =
    inherit RadioButton_DemoXaml()
    
    do
        this.InitializeComponent()
