Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("data", "display", "grid", "table", "binding")>
    Partial Public Class DataGrid_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            ' Populate the data grid with the list of planets:
            Me.DataGrid1.ItemsSource = AtomicElements.Elements
        End Sub
    End Class
End Namespace
