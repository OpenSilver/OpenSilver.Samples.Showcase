Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Binding1_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Dim listOfPlanets As ObservableCollection(Of Planet) = Planet.GetListOfPlanets()
            Me.ItemsControl1.ItemsSource = listOfPlanets
            Me.ContentControl1.Content = listOfPlanets(0)
        End Sub

        Private Sub ButtonPlanet_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim planet = CType(sender, Button).DataContext
            Me.ContentControl1.Content = planet
        End Sub
    End Class
End Namespace
