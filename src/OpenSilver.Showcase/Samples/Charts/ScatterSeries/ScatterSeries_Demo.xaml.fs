namespace OpenSilver.Showcase

open OpenSilver.Showcase.Other
open OpenSilver.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "points")>]
type ScatterSeries_Demo() as this =
    inherit ScatterSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.ChairsSeries.ItemsSource <- Sales.Chairs;
        this.TablesSeries.ItemsSource <- Sales.Tables;
