Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("click", "interaction", "command")>
    Partial Public Class PropertyChangedTrigger_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.borderText.Text = If(Equals(Me.borderText.Text, "Yellow"), "Orange", "Yellow")
        End Sub
    End Class
End Namespace
