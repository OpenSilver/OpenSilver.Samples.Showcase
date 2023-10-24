Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class DataGrid_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            ' Populate the data grid with the list of planets:
            Me.DataGrid1.ItemsSource = Planet.GetListOfPlanets()
        End Sub
    End Class
End Namespace
