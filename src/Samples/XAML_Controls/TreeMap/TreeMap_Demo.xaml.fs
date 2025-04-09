namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search
open System

[<SearchKeywords("hierarchy", "rectangle", "data", "proportional", "area", "visualization")>]
type TreeMap_Demo() as this =
    inherit TreeMap_DemoXaml()
    do
        this.InitializeComponent()
