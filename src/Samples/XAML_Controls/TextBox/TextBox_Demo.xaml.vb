Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class TextBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Your name is: " & Me.TextBoxName.Text)
        End Sub
    End Class
End Namespace
