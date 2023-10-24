Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class MethodToUpdateDom_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonSetCSS_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim div = Interop.GetDiv(Me)

            Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div)

            ' Note: refer to the documentation at: http://opensilver.net/links/how-to-call-javascript.aspx
        End Sub
    End Class
End Namespace
