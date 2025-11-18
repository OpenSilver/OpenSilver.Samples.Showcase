namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("attached properties", "XAML", "UI")>]
type AttachedProperties_Demo() as this =
    inherit AttachedProperties_DemoXaml()

    do
        this.InitializeComponent()
