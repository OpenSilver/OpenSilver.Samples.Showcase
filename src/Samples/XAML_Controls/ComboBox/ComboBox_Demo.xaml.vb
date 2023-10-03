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
    Partial Public Class ComboBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.ComboBox1.ItemsSource = Planet.GetListOfPlanets()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ComboBox_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ComboBox_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ComboBox_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DefaultComboBoxStyle.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/Styles/DefaultComboBoxStyle.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Planet.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Planet.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Other/Planet.vb"
    }
})
        End Sub

    End Class
End Namespace
