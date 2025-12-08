Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase

    <SearchKeywords("drag", "scroll")>
    Partial Public Class Thumb_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnThumbDragStarted(sender As Object, e As DragStartedEventArgs)
            infoTextBlock.Text = $"DragStarted X: {e.HorizontalOffset}; Y: {e.VerticalOffset}"
            Cursor = Cursors.ScrollAll
        End Sub

        Private Sub OnThumbDragDelta(sender As Object, e As DragDeltaEventArgs)
            infoTextBlock.Text = $"DragDelta X: {e.HorizontalChange}; Y: {e.VerticalChange}"
        End Sub

        Private Sub OnThumbDragCompleted(sender As Object, e As DragCompletedEventArgs)
            infoTextBlock.Text = $"DragCompleted X: {e.HorizontalChange}; Y: {e.VerticalChange}"
            Cursor = Nothing
        End Sub

    End Class

End Namespace
