Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("mouse", "scrolling", "interaction", "input", "event")>
    Partial Public Class MouseWheel_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.TitleContentControl.Content = "MouseWheel Event"
            AddHandler Me.ScrollBorder.MouseWheel, AddressOf Me.CountTotalScrollingDistance
        End Sub

        Private scrollingDistance As Integer = 0

        Public Sub CountTotalScrollingDistance(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
            e.Handled = True

            Dim delta = e.Delta

            scrollingDistance += delta
            Me.ScrollingDistanceTextBlock.Text = "Distance scrolled (with the mouse) on the border below: " & scrollingDistance.ToString() & "."
        End Sub
    End Class
End Namespace
