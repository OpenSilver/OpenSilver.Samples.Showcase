Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("input", "keypress", "event", "interaction")>
    Partial Public Class Keyboard_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub TextBoxInput_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If e.Key = Key.Enter Then MessageBox.Show("You pressed Enter!" & Environment.NewLine & Environment.NewLine & "This is the text that you entered: " & Me.TextBoxInput.Text)
        End Sub
    End Class
End Namespace
