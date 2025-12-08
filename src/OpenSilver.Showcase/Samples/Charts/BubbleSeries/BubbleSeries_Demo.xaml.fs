namespace OpenSilver.Showcase

open OpenSilver.Showcase.Other
open OpenSilver.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "bubble", "points")>]
type BubbleSeries_Demo() as this =
    inherit BubbleSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.ChairsSeries.ItemsSource <- Sales.Chairs;
        this.TablesSeries.ItemsSource <- Sales.Tables;
