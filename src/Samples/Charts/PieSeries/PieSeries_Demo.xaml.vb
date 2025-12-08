Imports OpenSilver.Samples.Showcase.Other
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("chart", "data", "visualization", "graph", "plot", "percentage")>
    Partial Public Class PieSeries_Demo
        Inherits ChartDemo

        Public Sub New()
            InitializeComponent()
            CostsSeries.ItemsSource = Sales.ProductionCosts
        End Sub
    End Class
End Namespace
