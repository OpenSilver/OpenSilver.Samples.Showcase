namespace OpenSilver.Showcase

open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("input", "selection", "calendar")>]
type public DatePicker_Demo() as this =
    inherit DatePicker_DemoXaml()

    do
        this.InitializeComponent()
