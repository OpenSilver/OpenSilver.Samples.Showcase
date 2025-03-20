Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("clipboard", "copy", "paste", "text", "interaction")>
    Partial Public Class Clipboard_SetText_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Await Clipboard.SetTextAsync(Me.ClipboardTextBox.Text)
            MessageBox.Show("Text copied!")
        End Sub
    End Class
End Namespace
