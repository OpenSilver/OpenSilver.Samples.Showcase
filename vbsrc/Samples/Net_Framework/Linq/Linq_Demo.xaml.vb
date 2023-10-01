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

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Partial Class Linq_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonToDemonstrateLinq_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim planets = Planet.GetListOfPlanets()

            Dim result = From p In planets Where p.Radius > 7000 Order By p.Name Select p.Name

            MessageBox.Show(String.Format("List of planets that have a radius greater than 7000km sorted alphabetically: {0}", String.Join(", ", result)))
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Linq_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Linq/Linq_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Linq_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Linq/Linq_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Planet.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Linq_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/Linq/Linq_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Planet.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Other/Planet.vb"
    }
})
        End Sub

    End Class
End Namespace
