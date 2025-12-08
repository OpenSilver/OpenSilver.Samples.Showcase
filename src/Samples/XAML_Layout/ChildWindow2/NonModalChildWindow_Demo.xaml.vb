Imports System.Threading
Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search
Imports PreviewOnWinRT

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("layout", "window", "popup", "nonmodal", "non-modal", "dialog")>
    Partial Public Class NonModalChildWindow_Demo
        Inherits UserControl
        Private _n As Integer = 1
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ButtonTestChildWindow_Modal_Click(sender As Object, e As RoutedEventArgs)
            Dim childWindow As New SmallChildWindow With {
                .Title = "ChildWindow (Modal) #" & Math.Min(Interlocked.Increment(_n), _n - 1).ToString(),
                .IsModal = True
            }
            childWindow.Show()
        End Sub
        Private Sub ButtonTestChildWindow_NonModal_Click(sender As Object, e As RoutedEventArgs)
            Dim childWindow As New SmallChildWindow With {
                .Title = "ChildWindow (Non-Modal) #" & Math.Min(Interlocked.Increment(_n), _n - 1).ToString(),
                .IsModal = False
            }
            childWindow.optionsStack.Visibility = Visibility.Collapsed
            childWindow.Show()
        End Sub
    End Class
End Namespace
