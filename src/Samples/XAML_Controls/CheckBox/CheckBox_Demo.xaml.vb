Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class CheckBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub CheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("You checked me.")
        End Sub

        Private Sub CheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("You unchecked me.")
        End Sub
    End Class
End Namespace
