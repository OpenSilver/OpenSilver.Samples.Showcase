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
    Partial Public Class PasswordBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DisplayPasswordIfNotEmpty()
        End Sub

        Private Sub DisplayPasswordIfNotEmpty()
            If Me.PasswordBox.Password.Length <> 0 Then
                MessageBox.Show("The password typed is " & Microsoft.VisualBasic.Constants.vbLf & """" & Me.PasswordBox.Password & """")
            Else
                MessageBox.Show("Please enter a password")
            End If
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "PasswordBox_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "PasswordBox_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "PasswordBox_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Controls/PasswordBox/PasswordBox_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
