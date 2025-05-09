Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("navigation", "navigationservice", "page", "content", "view", "container", "query", "urimapping", "urimapper", "url", "back", "forward")>
    Partial Public Class Frame_Demo
        Inherits UserControl

        Private _page As Page

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            _page = GetParentPage(Me)
            UpdateButtonsState()
        End Sub

        Private Sub UpdateButtonsState()
            ButtonBackwards.IsEnabled = _page.NavigationService.CanGoBack
            ButtonForward.IsEnabled = _page.NavigationService.CanGoForward
        End Sub

        Private Sub ButtonBackwards_Click(sender As Object, e As RoutedEventArgs)
            If _page.NavigationService.CanGoBack Then
                _page.NavigationService.GoBack()
            End If
        End Sub

        Private Sub ButtonForward_Click(sender As Object, e As RoutedEventArgs)
            If _page.NavigationService.CanGoForward Then
                _page.NavigationService.GoForward()
            End If
        End Sub

        Private Shared Function GetParentPage(child As DependencyObject) As Page
            Dim parent As DependencyObject = child
            While parent IsNot Nothing AndAlso Not TypeOf parent Is Page
                parent = VisualTreeHelper.GetParent(parent)
            End While
            Return TryCast(parent, Page)
        End Function

    End Class

End Namespace
