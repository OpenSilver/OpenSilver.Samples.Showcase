Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Interop_HtmlPresenter_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonClickHere_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call MessageBox.Show("The value is: " & Me.NumericTextBox1.Value.ToString())
        End Sub
    End Class
End Namespace
