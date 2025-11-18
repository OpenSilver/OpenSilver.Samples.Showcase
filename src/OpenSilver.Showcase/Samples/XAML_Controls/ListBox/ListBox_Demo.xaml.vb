Imports OpenSilver.Showcase.Search
Imports System.Windows.Controls

Namespace Global.OpenSilver.Showcase
    <SearchKeywords("input", "selection", "list", "items", "choices")>
    Partial Public Class ListBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.ListBox1.ItemsSource = Planet.GetListOfPlanets()
        End Sub
    End Class
End Namespace
