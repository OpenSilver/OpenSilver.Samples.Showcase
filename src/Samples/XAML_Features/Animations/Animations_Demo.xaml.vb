Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("animation", "effects", "motion", "behavior")>
    Partial Public Class Animations_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonToStartAnimationOpen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim storyboard = CType(Me.CanvasForAnimationsDemo.Resources("AnimationToOpen"), Storyboard)
            storyboard.Begin()
            Me.ButtonToStartAnimationOpen.Visibility = Visibility.Collapsed
            Me.ButtonToStartAnimationClose.Visibility = Visibility.Visible
        End Sub

        Private Sub ButtonToStartAnimationClose_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim storyboard = CType(Me.CanvasForAnimationsDemo.Resources("AnimationToClose"), Storyboard)
            storyboard.Begin()
            Me.ButtonToStartAnimationOpen.Visibility = Visibility.Visible
            Me.ButtonToStartAnimationClose.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
