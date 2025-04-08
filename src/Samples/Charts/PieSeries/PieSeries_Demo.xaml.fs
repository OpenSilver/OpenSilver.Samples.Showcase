namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Other
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("chart", "data", "visualization", "graph", "plot", "percentage")>]
type PieSeries_Demo() as this =
    inherit PieSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.CostsSeries.ItemsSource <- Sales.ProductionCosts
