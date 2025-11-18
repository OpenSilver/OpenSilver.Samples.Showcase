namespace OpenSilver.Showcase

open System
open System.Globalization
open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("input", "counter", "control", "buttonspinner")>]
type public UpDownBase_Demo() as this =
    inherit UpDownBase_DemoXaml()

    do
        this.InitializeComponent()

