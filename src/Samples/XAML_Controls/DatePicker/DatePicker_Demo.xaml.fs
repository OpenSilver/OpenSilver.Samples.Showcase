namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "selection", "calendar")>]
type public DatePicker_Demo() as this =
    inherit DatePicker_DemoXaml()

    do
        this.InitializeComponent()
