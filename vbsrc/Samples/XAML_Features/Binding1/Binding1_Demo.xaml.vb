Imports System.Collections.Generic
Imports System.Collections.ObjectModel
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
    Public Partial Class Binding1_Demo
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

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Binding1_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding1/Binding1_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Binding1_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding1/Binding1_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Binding1_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Features/Binding1/Binding1_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Planet.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Planet.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Other/Planet.vb"
    }
})
        End Sub

    End Class
End Namespace
