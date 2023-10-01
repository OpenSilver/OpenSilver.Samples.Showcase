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

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Partial Class FindElementsInHostCoordinates_Demo
        Inherits UserControl
        Private _highestZIndex As Integer

        Public Sub New()
            Me.InitializeComponent()

            InitAllZIndex()
            _highestZIndex = 2

#If SLMIGRATION
            AddHandler MouseLeftButtonDown, AddressOf FindElementsInHostCoordinates_Demo_PointerPressed
#Else
            this.PointerPressed += FindElementsInHostCoordinates_Demo_PointerPressed; 
#End If
        End Sub

#If SLMIGRATION
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

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FindElementsInHostCoordinates_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FindElementsInHostCoordinates_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FindElementsInHostCoordinates_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml.vb"
    }
})
        End Sub


    End Class
End Namespace
