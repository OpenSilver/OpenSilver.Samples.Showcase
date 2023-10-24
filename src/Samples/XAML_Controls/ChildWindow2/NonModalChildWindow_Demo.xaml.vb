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
    End Class
End Namespace
