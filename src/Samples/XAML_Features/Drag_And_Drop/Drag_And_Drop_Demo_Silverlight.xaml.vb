Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Drag_And_Drop_Demo
        Inherits UserControl
        Private _isPointerCaptured As Boolean
        Private _pointerX As Double
        Private _pointerY As Double
        Private _objectLeft As Double
        Private _objectTop As Double

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub DragAndDropItem_PointerPressed(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim uielement = CType(sender, UIElement)
            _objectLeft = Canvas.GetLeft(uielement)
            _objectTop = Canvas.GetTop(uielement)

            _pointerX = e.GetPosition(Nothing).X
            _pointerY = e.GetPosition(Nothing).Y
            uielement.CaptureMouse()
            _isPointerCaptured = True
        End Sub

        Private Sub DragAndDropItem_PointerMoved(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim uielement = CType(sender, UIElement)
            If _isPointerCaptured Then
                ' Calculate the new position of the object:
                Dim deltaH = e.GetPosition(Nothing).X - _pointerX
                Dim deltaV = e.GetPosition(Nothing).Y - _pointerY
                _objectLeft = deltaH + _objectLeft
                _objectTop = deltaV + _objectTop

                ' Update the object position:
                Canvas.SetLeft(uielement, _objectLeft)
                Canvas.SetTop(uielement, _objectTop)

                ' Remember the pointer position:
                _pointerX = e.GetPosition(Nothing).X
                _pointerY = e.GetPosition(Nothing).Y
            End If
        End Sub

        Private Sub DragAndDropItem_PointerReleased(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim uielement = CType(sender, UIElement)
            _isPointerCaptured = False
            uielement.ReleaseMouseCapture()
        End Sub
    End Class
End Namespace
