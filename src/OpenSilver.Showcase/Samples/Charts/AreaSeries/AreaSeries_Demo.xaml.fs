namespace OpenSilver.Showcase

open OpenSilver.Showcase.Other
open OpenSilver.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "points")>]
type AreaSeries_Demo() as this =
    inherit AreaSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.ChairsSeries.ItemsSource <- Sales.Chairs;
        this.TablesSeries.ItemsSource <- Sales.Tables;
