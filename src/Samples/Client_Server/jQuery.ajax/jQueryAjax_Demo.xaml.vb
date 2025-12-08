Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase

    Partial Public Class jQueryAjax_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Try
                Dim result = Await Extensions.MakeAjaxCall(url:="http://fiddle.jshell.net/echo/html/", data:="some sample text", type:="post")

                MessageBox.Show("The server returned the following result: " & result)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub
    End Class
End Namespace