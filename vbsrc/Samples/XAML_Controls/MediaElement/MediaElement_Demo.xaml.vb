Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
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
    Partial Public Class MediaElement_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "MediaElement_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/MediaElement/MediaElement_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "MediaElement_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/MediaElement/MediaElement_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "MediaElement_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Controls/MediaElement/MediaElement_Demo.xaml.vb"
    }
})
        End Sub

        Private Sub ButtonToPlayAudio_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForAudio.Play()
        End Sub

        Private Sub ButtonToPauseAudio_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForAudio.Pause()
        End Sub

        Private Sub ButtonToPlayVideo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForVideo.Play()
        End Sub

        Private Sub ButtonToPauseVideo_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MediaElementForVideo.Pause()
        End Sub
    End Class
End Namespace
