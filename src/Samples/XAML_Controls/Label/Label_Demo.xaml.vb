Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Label_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonViewMore_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            'ChildWindowHelper.ShowChildWindow(new Button_Demo_More());
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MessageBox.Show("You clicked the button!")
        End Sub
    End Class
End Namespace
