namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("text", "display", "label", "content", "UI", "paragraph", "run", "hyperlink")>]
type RichTextBlock_Demo() as this =
    inherit RichTextBlock_DemoXaml()

    do
        this.InitializeComponent()
