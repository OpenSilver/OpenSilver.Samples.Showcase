Imports System.Windows
Imports System.Windows.Controls

Namespace PreviewOnWinRT
    Partial Public Class SmallChildWindow
        Inherits ChildWindow
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
        End Sub

        Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = False
        End Sub
    End Class
End Namespace
