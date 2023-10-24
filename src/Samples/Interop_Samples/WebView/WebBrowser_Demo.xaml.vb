Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class WebView_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonNavigate_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.WebView1.Navigate(New Uri(Me.TextBoxWithURL.Text))
        End Sub
    End Class
End Namespace
