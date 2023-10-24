Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class ContextMenu_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub MenuItem1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Menu Item 1")
        End Sub

        Private Sub MenuItem2_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Menu Item 2")
        End Sub

        Private Sub MenuItem3_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("Menu Item 3")
        End Sub
    End Class
End Namespace
