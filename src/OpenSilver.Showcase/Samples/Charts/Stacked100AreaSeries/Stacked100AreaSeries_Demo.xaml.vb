Imports OpenSilver.Showcase.Other
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase
    <SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "stacked", "points")>
    Partial Public Class Stacked100AreaSeries_Demo
        Inherits ChartDemo

        Public Sub New()
            InitializeComponent()
            ChairsSeries.ItemsSource = Sales.Chairs
            TablesSeries.ItemsSource = Sales.Tables
        End Sub
    End Class
End Namespace
