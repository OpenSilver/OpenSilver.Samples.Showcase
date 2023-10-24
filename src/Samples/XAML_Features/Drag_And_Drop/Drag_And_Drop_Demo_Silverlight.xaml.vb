Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

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

#If SLMIGRATION Then
#Else
        void DragAndDropItem_PointerPressed(object sender, PointerRoutedEventArgs e)
#End If
        Private Sub DragAndDropItem_PointerPressed(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim uielement = CType(sender, UIElement)
            _objectLeft = Canvas.GetLeft(uielement)
            _objectTop = Canvas.GetTop(uielement)

#If SLMIGRATION Then
            _pointerX = e.GetPosition(Nothing).X
            _pointerY = e.GetPosition(Nothing).Y
            uielement.CaptureMouse()
#Else
            _pointerX = e.GetCurrentPoint(null).Position.X;
            _pointerY = e.GetCurrentPoint(null).Position.Y;
            uielement.CapturePointer(e.Pointer);
#End If
            _isPointerCaptured = True
        End Sub

#If SLMIGRATION Then
#Else
        void DragAndDropItem_PointerMoved(object sender, PointerRoutedEventArgs e)
#End If
        Private Sub DragAndDropItem_PointerMoved(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim uielement = CType(sender, UIElement)
            If _isPointerCaptured Then
                ' Calculate the new position of the object:
#If SLMIGRATION Then
                Dim deltaH = e.GetPosition(Nothing).X - _pointerX
                Dim deltaV = e.GetPosition(Nothing).Y - _pointerY
#Else
                double deltaH = e.GetCurrentPoint(null).Position.X - _pointerX;
                double deltaV = e.GetCurrentPoint(null).Position.Y - _pointerY;
#End If
                _objectLeft = deltaH + _objectLeft
                _objectTop = deltaV + _objectTop

                ' Update the object position:
                Canvas.SetLeft(uielement, _objectLeft)
                Canvas.SetTop(uielement, _objectTop)

                ' Remember the pointer position:
#If SLMIGRATION Then
                _pointerX = e.GetPosition(Nothing).X
                _pointerY = e.GetPosition(Nothing).Y
#Else
                _pointerX = e.GetCurrentPoint(null).Position.X;
                _pointerY = e.GetCurrentPoint(null).Position.Y;
#End If
            End If
        End Sub

#If SLMIGRATION Then
#Else
        void DragAndDropItem_PointerReleased(object sender, PointerRoutedEventArgs e)
#End If
        Private Sub DragAndDropItem_PointerReleased(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim uielement = CType(sender, UIElement)
            _isPointerCaptured = False
#If SLMIGRATION Then
            uielement.ReleaseMouseCapture()
#Else
            uielement.ReleasePointerCapture(e.Pointer);
#End If
        End Sub
    End Class
End Namespace
