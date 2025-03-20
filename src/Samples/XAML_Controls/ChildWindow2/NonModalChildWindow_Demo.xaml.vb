Imports OpenSilver.Samples.Showcase.Search
Imports PreviewOnWinRT
Imports System.Threading
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("layout", "window", "popup", "nonmodal", "non-modal", "dialog")>
    Partial Public Class NonModalChildWindow_Demo
        Inherits UserControl
        Private _n As Integer
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonTestChildWindow_Modal_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim childWindow As SmallChildWindow = New SmallChildWindow()
            childWindow.Title = "ChildWindow (Modal)" & Math.Min(Interlocked.Increment(_n), _n - 1).ToString()
#If Not OPENSILVER Then
            childWindow.IsModal = true; 
#End If
            childWindow.Show()
        End Sub
        Private Sub ButtonTestChildWindow_NonModal_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim childWindow As SmallChildWindow = New SmallChildWindow()
            childWindow.Title = "ChildWindow (Non-modal)" & Math.Min(Interlocked.Increment(_n), _n - 1).ToString()
#If Not OPENSILVER Then
            childWindow.IsModal = false; 
#End If
            childWindow.Show()
        End Sub
    End Class
End Namespace
