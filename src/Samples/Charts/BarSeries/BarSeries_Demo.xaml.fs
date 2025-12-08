namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Other
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "bar")>]
type BarSeries_Demo() as this =
    inherit BarSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.ChairsSeries.ItemsSource <- Sales.Chairs;
        this.TablesSeries.ItemsSource <- Sales.Tables;
