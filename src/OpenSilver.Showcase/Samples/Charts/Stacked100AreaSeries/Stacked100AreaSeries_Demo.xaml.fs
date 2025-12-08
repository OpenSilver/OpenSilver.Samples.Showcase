namespace OpenSilver.Showcase

open OpenSilver.Showcase.Other
open OpenSilver.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "stacked", "points")>]
type public Stacked100AreaSeries_Demo() as this =
    inherit Stacked100AreaSeries_DemoXaml()

    do
        this.InitializeComponent()
        this.ChairsSeries.ItemsSource <- Sales.Chairs
        this.TablesSeries.ItemsSource <- Sales.Tables
