Imports System.Collections.Generic
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
    Partial Public Class DataGrid_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            ' Populate the data grid with the list of planets:
            Me.DataGrid1.ItemsSource = Planet.GetListOfPlanets()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DataGrid_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DataGrid/DataGrid_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DataGrid_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DataGrid/DataGrid_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DataGrid_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DataGrid/DataGrid_Demo.xaml.vb"
    }
})
        End Sub
    End Class
End Namespace
