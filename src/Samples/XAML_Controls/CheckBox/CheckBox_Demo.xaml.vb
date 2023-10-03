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
    Partial Public Class CheckBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub CheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("You checked me.")
        End Sub

        Private Sub CheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("You unchecked me.")
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "CheckBox_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "CheckBox_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "CheckBox_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/CheckBox/CheckBox_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
