Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase

    <SearchKeywords("animation")>
    Partial Public Class TransitioningContentControl_Demo
        Inherits UserControl

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
