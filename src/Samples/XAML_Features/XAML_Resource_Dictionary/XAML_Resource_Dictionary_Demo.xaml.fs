namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("XAML", "resources", "dictionary", "styles", "UI")>]
type XAML_Resource_Dictionary_Demo() as this =
    inherit XAML_Resource_Dictionary_DemoXaml()
    
    do
        this.InitializeComponent()
