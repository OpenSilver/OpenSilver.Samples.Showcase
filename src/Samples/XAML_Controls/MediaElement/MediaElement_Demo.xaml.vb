Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class MediaElement_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonToPlayAudio_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForAudio.Play()
        End Sub

        Private Sub ButtonToPauseAudio_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForAudio.Pause()
        End Sub

        Private Sub ButtonToPlayVideo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForVideo.Play()
        End Sub

        Private Sub ButtonToPauseVideo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForVideo.Pause()
        End Sub
    End Class
End Namespace
