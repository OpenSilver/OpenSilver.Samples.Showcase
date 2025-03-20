Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("popup", "modal", "window", "overlay", "UI")>
    Partial Public Class Popup_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OpenPopupButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MyPopup.IsOpen = True
        End Sub

        Private Sub PopupButtonClose_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.MyPopup.IsOpen = False
        End Sub
    End Class
End Namespace
