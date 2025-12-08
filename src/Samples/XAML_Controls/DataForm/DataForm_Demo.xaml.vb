Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("form", "input", "editor", "data", "binding")>
    Partial Public Class DataForm_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
            'Populate the data form with the list of planets:
            DataForm1.ItemsSource = Planet.GetListOfPlanets()
        End Sub
    End Class
End Namespace
