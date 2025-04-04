Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("animation")>
    Partial Public Class TransitioningContentControl_Demo
        Inherits ContentControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ChangeContentWithDefaultTransition(sender As Object, e As RoutedEventArgs)
            SetContentWithTransition(TransitioningContentControl.DefaultTransitionState)
        End Sub

        Private Sub ChangeContentWithDownTransition(sender As Object, e As RoutedEventArgs)
            SetContentWithTransition("DownTransition")
        End Sub

        Private Sub ChangeContentWithUpTransition(sender As Object, e As RoutedEventArgs)
            SetContentWithTransition("UpTransition")
        End Sub

        Private Sub SetContentWithTransition(transition As String)
            defaultTCC.Transition = transition
            defaultTCC.Content = DateTime.Now.Ticks
        End Sub

    End Class

End Namespace
