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

Namespace OpenSilver.Samples.Showcase
    Partial Public Class MouseWheel_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If SLMIGRATION Then
            Me.TitleContentControl.Content = "MouseWheel Event"
            AddHandler Me.ScrollBorder.MouseWheel, AddressOf Me.CountTotalScrollingDistance
#Else
            TitleContentControl.Content = "PointerWheelChanged Event";
            ScrollBorder.PointerWheelChanged += CountTotalScrollingDistance;
#End If
        End Sub

        Private scrollingDistance As Integer = 0

#If SLMIGRATION Then
        Public Sub CountTotalScrollingDistance(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
#Else
        Public Sub CountTotalScrollingDistance(ByVal sender As Object, ByVal e As PointerRoutedEventArgs)
#End If

            e.Handled = True

#If SLMIGRATION Then
            Dim delta = e.Delta
#Else
            int delta = e.GetCurrentPoint(null).Properties.MouseWheelDelta;
#End If

            scrollingDistance += delta
            Me.ScrollingDistanceTextBlock.Text = "Distance scrolled (with the mouse) on the border below: " & scrollingDistance.ToString() & "."
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call Showcase.ViewSourceButtonHelper.ViewSource(New List(Of OpenSilver.Samples.Showcase.ViewSourceButtonInfo)() From {
                    New Showcase.ViewSourceButtonInfo() With {
        .TabHeader = "MouseWheel_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml"
    },
                    New Showcase.ViewSourceButtonInfo() With {
        .TabHeader = "MouseWheel_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.cs"
    },
                    New Showcase.ViewSourceButtonInfo() With {
        .TabHeader = "MouseWheel_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.vb"
    }
})
        End Sub
    End Class
End Namespace
