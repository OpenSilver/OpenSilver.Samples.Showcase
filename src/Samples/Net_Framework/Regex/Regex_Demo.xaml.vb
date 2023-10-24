Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Regex_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonReplaceDates_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.TextBlockOutputOfRegexReplaceDemo.Text = Regex.Replace(Me.TextBoxRegexReplaceDemo.Text, "(\d{2})/(\d{2})/(\d{2,4})", "$3-$2-$1")
        End Sub
    End Class
End Namespace
