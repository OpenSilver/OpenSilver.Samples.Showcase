Imports System.Windows.Browser
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class HtmlPage_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonGetCurrentURL_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("The current URL is: " & HtmlPage.Document.DocumentUri.OriginalString)
        End Sub
    End Class
End Namespace
