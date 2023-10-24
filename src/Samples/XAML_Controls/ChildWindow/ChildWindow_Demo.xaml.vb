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

Namespace OpenSilver.Samples.Showcase
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
    End Class
End Namespace
