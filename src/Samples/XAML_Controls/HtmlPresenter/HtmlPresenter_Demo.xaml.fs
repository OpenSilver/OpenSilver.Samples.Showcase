namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("html", "web", "content", "rendering", "UI")>]
type HtmlPresenter_Demo() as this =
    inherit HtmlPresenter_DemoXaml()

    do
        this.InitializeComponent()
