namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "text", "entry", "form", "user input")>]
type TextBox_Demo() as this =
    inherit TextBox_DemoXaml()
    
    do
        this.InitializeComponent()
