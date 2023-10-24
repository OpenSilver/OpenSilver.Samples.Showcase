Imports System.Collections.Generic
Imports System.Linq
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media
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
    Partial Public Class FindElementsInHostCoordinates_Demo
        Inherits UserControl
        Private _highestZIndex As Integer

        Public Sub New()
            Me.InitializeComponent()

            InitAllZIndex()
            _highestZIndex = 2

#If SLMIGRATION Then
            AddHandler MouseLeftButtonDown, AddressOf FindElementsInHostCoordinates_Demo_PointerPressed
#Else
            this.PointerPressed += FindElementsInHostCoordinates_Demo_PointerPressed; 
#End If
        End Sub

#If SLMIGRATION Then
        Private Sub FindElementsInHostCoordinates_Demo_PointerPressed(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            ' Get the absolute coordinates of the pointer:
            Dim currentPoint = e.GetPosition(Nothing)

#Else
        void FindElementsInHostCoordinates_Demo_PointerPressed(object sender, PointerRoutedEventArgs e) 
        {
            // Get the absolute coordinates of the pointer:
            Point currentPoint = e.GetCurrentPoint(null).Position;
#End If

            ' Find the element that is under the pointer:
            Dim uiElement = VisualTreeHelper.FindElementsInHostCoordinates(currentPoint, Me.CanvasParent).FirstOrDefault()

            ' Bring the clicked element to the front:
            If TypeOf uiElement Is Border Then
                _highestZIndex += 1
                Canvas.SetZIndex(uiElement, _highestZIndex)
            End If
        End Sub

        Private Sub InitAllZIndex()
            ' 0 -> 2 is from the background to the front
            Canvas.SetZIndex(Me.BlueRectangle, 0)
            Canvas.SetZIndex(Me.GreenRectangle, 1)
            Canvas.SetZIndex(Me.YellowRectangle, 2)
        End Sub
    End Class
End Namespace
