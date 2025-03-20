Imports OpenSilver.Samples.Showcase.Other
Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("chart", "data", "visualization", "graph", "plot", "column", "bar")>
    Partial Public Class ColumnSeries_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            ChairsSeries.ItemsSource = Sales.Chairs
            TablesSeries.ItemsSource = Sales.Tables
        End Sub
    End Class
End Namespace
