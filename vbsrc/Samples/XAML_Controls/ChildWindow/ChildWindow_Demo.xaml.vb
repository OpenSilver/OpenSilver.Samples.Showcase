Imports PreviewOnWinRT
Imports System
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
    Partial Public Class ChildWindow_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub


        Private Sub ButtonTestChildWindow_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim loginWnd As LoginWindow = New LoginWindow()
            AddHandler loginWnd.Closed, New EventHandler(AddressOf loginWnd_Closed)
            loginWnd.Show()
        End Sub

        Private Sub loginWnd_Closed(ByVal sender As Object, ByVal e As EventArgs)
            Dim lw = CType(sender, LoginWindow)
            If lw.DialogResult.HasValue AndAlso lw.DialogResult.Value = True AndAlso Not Equals(lw.NameBox.Text, String.Empty) Then
                Me.TextBlockForTestingChildWindow.Text = "Hello " & lw.NameBox.Text
            Else
                Me.TextBlockForTestingChildWindow.Text = "Login cancelled."
            End If
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ChildWindow_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ChildWindow_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml.cs"
    }, New ViewSourceButtonInfo() With {
        .TabHeader = "ChildWindow_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "LoginWindow.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "LoginWindow.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml.cs"
    }, New ViewSourceButtonInfo() With {
        .TabHeader = "LoginWindow.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DefaultChildWindowStyle.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/Styles/DefaultChildWindowStyle.xaml"
    }
})
        End Sub

    End Class
End Namespace
