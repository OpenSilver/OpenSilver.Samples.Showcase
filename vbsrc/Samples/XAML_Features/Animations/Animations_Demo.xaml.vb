Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Animations_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonToStartAnimationOpen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim storyboard = CType(Me.CanvasForAnimationsDemo.Resources("AnimationToOpen"), Storyboard)
            storyboard.Begin()
            Me.ButtonToStartAnimationOpen.Visibility = Visibility.Collapsed
            Me.ButtonToStartAnimationClose.Visibility = Visibility.Visible
        End Sub

        Private Sub ButtonToStartAnimationClose_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim storyboard = CType(Me.CanvasForAnimationsDemo.Resources("AnimationToClose"), Storyboard)
            storyboard.Begin()
            Me.ButtonToStartAnimationOpen.Visibility = Visibility.Visible
            Me.ButtonToStartAnimationClose.Visibility = Visibility.Collapsed
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Animations_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Animations_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Animations_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Features/Animations/Animations_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
