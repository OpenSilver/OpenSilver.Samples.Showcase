namespace OpenSilver.Showcase

open OpenSilver.Showcase.Other
open OpenSilver.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "percentage")>]
type PieSeries_Demo() as this =
    inherit PieSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.CostsSeries.ItemsSource <- Sales.ProductionCosts
