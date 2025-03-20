Imports OpenSilver.Samples.Showcase.Other
Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "stacked", "points")>
    Partial Public Class Stacked100AreaSeries_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            ChairsSeries.ItemsSource = Sales.Chairs
            TablesSeries.ItemsSource = Sales.Tables
        End Sub
    End Class
End Namespace
