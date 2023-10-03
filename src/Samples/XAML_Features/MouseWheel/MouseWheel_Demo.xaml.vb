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
    Public Partial Class MouseWheel_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf MouseWheel_Loaded
            AddHandler Unloaded, AddressOf MouseWheel_Unloaded
        End Sub

        Private Sub MouseWheel_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

#If SLMIGRATION
            RemoveHandler Me.ScrollBorder.MouseWheel, AddressOf Me.CountTotalScrollingDistance
#Else
            ScrollBorder.PointerWheelChanged += CountTotalScrollingDistance; 
#End If
        End Sub

        Private Sub MouseWheel_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Interop.ExecuteJavaScript("$0.onwheel = (e) => {e.preventDefault(); return false;}", Interop.GetDiv(Me.ScrollBorder))
#If SLMIGRATION
            Me.TitleContentControl.Content = "MouseWheel Event"
            AddHandler Me.ScrollBorder.MouseWheel, AddressOf Me.CountTotalScrollingDistance
#Else
            TitleContentControl.Content = "PointerWheelChanged Event";
            ScrollBorder.PointerWheelChanged += CountTotalScrollingDistance;
#End If

        End Sub

        Private scrollingDistance As Integer = 0
#If SLMIGRATION
#Else
        public void CountTotalScrollingDistance(object sender, PointerRoutedEventArgs e)
#End If
        Public Sub CountTotalScrollingDistance(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
            Interop.ExecuteJavaScript("$0.preventDefault()", e.INTERNAL_OriginalJSEventArg)

#If SLMIGRATION
            Dim delta = e.Delta
#Else
            int delta = e.GetCurrentPoint(null).Properties.MouseWheelDelta;
#End If

            scrollingDistance += delta
            Me.ScrollingDistanceTextBlock.Text = "Distance scrolled (with the mouse) on the border below: " & scrollingDistance.ToString() & "."
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "MouseWheel_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "MouseWheel_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "MouseWheel_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.vb"
    }
})
        End Sub
    End Class
End Namespace
