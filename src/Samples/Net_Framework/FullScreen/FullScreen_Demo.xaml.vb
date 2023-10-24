Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class FullScreen_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Public Sub EnterFullScreen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Application.Current.Host.Content.IsFullScreen = True
        End Sub

        Public Sub ExitFullSCreen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Application.Current.Host.Content.IsFullScreen = False
        End Sub
    End Class
End Namespace
