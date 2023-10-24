Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class File_Open_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnFileOpened(ByVal sender As Object, ByVal e As Extensions.FileOpenDialog.FileOpenedEventArgs)
            MessageBox.Show(e.Text)
        End Sub
    End Class
End Namespace
