Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("input", "toggle", "option", "selection", "form")>
    Partial Public Class RadioButton_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub RadioButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dispatcher.BeginInvoke(Sub() MessageBox.Show(If(Me.RadioButton1.IsChecked = True, "Option 1 selected", "Option 2 selected")))
        End Sub
    End Class
End Namespace
