Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("behavior", "interaction", "draggable", "mousedrag", "mousedragelementbehavior", "fluidmove", "fluidmovebehavior", "datastatebehavior", "effects")>
    Partial Public Class Behaviors_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnMoveLeftButtonClick(sender As Object, e As RoutedEventArgs)
            Dim currentLeft As Double = Canvas.GetLeft(MovingRectangle)
            If currentLeft > 10 Then
                Canvas.SetLeft(MovingRectangle, currentLeft - 50)
            End If
        End Sub

        Private Sub OnMoveRightButtonClick(sender As Object, e As RoutedEventArgs)
            Dim currentLeft As Double = Canvas.GetLeft(MovingRectangle)
            If currentLeft < 150 Then
                Canvas.SetLeft(MovingRectangle, currentLeft + 50)
            End If
        End Sub

        Private Sub OnAddButtonClick(sender As Object, e As RoutedEventArgs)
            wrapPanel.Children.Insert(0, New Border())
            UpdateRemoveButtonState()
        End Sub

        Private Sub OnRemoveButtonClick(sender As Object, e As RoutedEventArgs)
            If wrapPanel.Children.Count > 0 Then
                wrapPanel.Children.RemoveAt(0)
                UpdateRemoveButtonState()
            End If
        End Sub

        Private Sub UpdateRemoveButtonState()
            RemoveButton.IsEnabled = wrapPanel.Children.Count > 0
        End Sub

    End Class

End Namespace
