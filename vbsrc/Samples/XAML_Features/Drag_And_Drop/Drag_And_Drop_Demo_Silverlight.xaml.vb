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

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Partial Class Drag_And_Drop_Demo
        Inherits UserControl
        Private _isPointerCaptured As Boolean
        Private _pointerX As Double
        Private _pointerY As Double
        Private _objectLeft As Double
        Private _objectTop As Double

        Public Sub New()
            Me.InitializeComponent()
        End Sub

#If SLMIGRATION
#Else
        void DragAndDropItem_PointerPressed(object sender, PointerRoutedEventArgs e)
#End If
        Private Sub DragAndDropItem_PointerPressed(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim uielement = CType(sender, UIElement)
            _objectLeft = Canvas.GetLeft(uielement)
            _objectTop = Canvas.GetTop(uielement)

#If SLMIGRATION
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

#If SLMIGRATION
#Else
        void DragAndDropItem_PointerMoved(object sender, PointerRoutedEventArgs e)
#End If
        Private Sub DragAndDropItem_PointerMoved(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim uielement = CType(sender, UIElement)
            If _isPointerCaptured Then
                ' Calculate the new position of the object:
#If SLMIGRATION
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
#If SLMIGRATION
                _pointerX = e.GetPosition(Nothing).X
                _pointerY = e.GetPosition(Nothing).Y
#Else
                _pointerX = e.GetCurrentPoint(null).Position.X;
                _pointerY = e.GetCurrentPoint(null).Position.Y;
#End If
            End If
        End Sub

#If SLMIGRATION
#Else
        void DragAndDropItem_PointerReleased(object sender, PointerRoutedEventArgs e)
#End If
        Private Sub DragAndDropItem_PointerReleased(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim uielement = CType(sender, UIElement)
            _isPointerCaptured = False
#If SLMIGRATION
            uielement.ReleaseMouseCapture()
#Else
            uielement.ReleasePointerCapture(e.Pointer);
#End If
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
