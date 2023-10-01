Imports PreviewOnWinRT
Imports System
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
    Partial Public Class NonModalChildWindow_Demo
        Inherits UserControl
        Private _n As Integer
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonTestChildWindow_Modal_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim childWindow As SmallChildWindow = New SmallChildWindow()
            childWindow.Title = "ChildWindow (Modal)" & Math.Min(Threading.Interlocked.Increment(_n), _n - 1).ToString()
#If Not OPENSILVER Then
            childWindow.IsModal = true; 
#End If
            childWindow.Show()
        End Sub
        Private Sub ButtonTestChildWindow_NonModal_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim childWindow As SmallChildWindow = New SmallChildWindow()
            childWindow.Title = "ChildWindow (Non-modal)" & Math.Min(Threading.Interlocked.Increment(_n), _n - 1).ToString()
#If Not OPENSILVER Then
            childWindow.IsModal = false; 
#End If
            childWindow.Show()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "NonModalChildWindow_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "NonModalChildWindow_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "NonModalChildWindow_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "SmallChildWindow.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "SmallChildWindow.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "SmallChildWindow.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DefaultChildWindowStyle.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/Styles/DefaultChildWindowStyle.xaml"
    }
})
        End Sub
    End Class
End Namespace
