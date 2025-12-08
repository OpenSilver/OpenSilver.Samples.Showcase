Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("JavaScript", "interop", "browser", "script")>
    Partial Public Class ExecuteJavaScript_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub SendJavaScriptMessage(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Interop.ExecuteJavaScript("alert($0);", Me.TextBoxInput.Text)
        End Sub
    End Class
End Namespace
