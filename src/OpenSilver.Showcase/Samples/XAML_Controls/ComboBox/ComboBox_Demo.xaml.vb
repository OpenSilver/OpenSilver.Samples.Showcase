Imports OpenSilver.Showcase.Search
Imports System.Windows.Controls

Namespace Global.OpenSilver.Showcase
    <SearchKeywords("input", "selection", "list", "dropdown", "choices")>
    Partial Public Class ComboBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.ComboBox1.ItemsSource = Planet.GetListOfPlanets()
        End Sub
    End Class
End Namespace
