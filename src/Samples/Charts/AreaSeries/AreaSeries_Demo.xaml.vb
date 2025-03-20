Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Other
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "points")>
    Partial Public Class AreaSeries_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            ChairsSeries.ItemsSource = Sales.Chairs
            TablesSeries.ItemsSource = Sales.Tables
        End Sub
    End Class
End Namespace
