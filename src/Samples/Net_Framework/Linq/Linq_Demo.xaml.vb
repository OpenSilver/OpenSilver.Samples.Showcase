Imports System.Collections.Generic
Imports System.Linq
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

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
