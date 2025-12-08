namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "text", "entry")>]
type RichTextBox_Demo() as this =
    inherit RichTextBox_DemoXaml()

    do
        this.InitializeComponent()
