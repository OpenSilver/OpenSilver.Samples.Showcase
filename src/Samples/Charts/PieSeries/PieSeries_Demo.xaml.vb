Imports OpenSilver.Samples.Showcase.Other
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    Partial Public Class PieSeries_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            CostsSeries.ItemsSource = Sales.ProductionCosts
        End Sub
    End Class
End Namespace
