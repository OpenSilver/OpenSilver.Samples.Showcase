Imports OpenSilver.Samples.Showcase.Other
Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("chart", "data", "visualization", "graph", "plot", "percentage")>
    Partial Public Class PieSeries_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            CostsSeries.ItemsSource = Sales.ProductionCosts
        End Sub
    End Class
End Namespace
