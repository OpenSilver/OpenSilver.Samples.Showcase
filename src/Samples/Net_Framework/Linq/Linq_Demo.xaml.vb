Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Linq_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonToDemonstrateLinq_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim planets = Planet.GetListOfPlanets()

            Dim result = From p In planets Where p.Radius > 7000 Order By p.Name Select p.Name

            MessageBox.Show(String.Format("List of planets that have a radius greater than 7000km sorted alphabetically: {0}", String.Join(", ", result)))
        End Sub
    End Class
End Namespace
