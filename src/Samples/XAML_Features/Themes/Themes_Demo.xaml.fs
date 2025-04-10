namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search
open System.Windows.Controls
open System

[<SearchKeywords("styling", "styles", "theme", "customization", "UI", "modern")>]
type Themes_Demo() as this =
    inherit Themes_DemoXaml()

    do
        this.InitializeComponent()
