Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace OpenSilver.Samples.Showcase
    Partial Public Class SearchPage
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            AddHandler SearchField.Loaded, AddressOf OnSearchFieldLoaded
            SearchField.AddHandler(KeyDownEvent, New KeyEventHandler(AddressOf SearchField_KeyDown), True)
        End Sub

        Private Sub OnSearchFieldLoaded(sender As Object, e As RoutedEventArgs)
            SearchField.Focus()

            ' Get the MainPage and start the search
            Dim mainPage As MainPage = TryCast(Application.Current.RootVisual, MainPage)
            If mainPage IsNot Nothing Then
                Dim originalPath As String = mainPage.PageContainer.CurrentSource.OriginalString
                If originalPath.Length > 8 Then
                    Dim searchTerms As String = originalPath.Substring(8) ' Remove "/Search/" from "Search/searchParameters"
                    If Not String.IsNullOrWhiteSpace(searchTerms) Then
                        SearchField.Text = searchTerms
                        PerformSearch(searchTerms)
                    End If
                End If
            End If
        End Sub

        Private Sub ButtonSearch_Click(sender As Object, e As RoutedEventArgs)
            Dim searchText As String = SearchField.Text
            PerformSearch(searchText)
        End Sub

        Private Sub SearchField_KeyDown(sender As Object, e As KeyEventArgs)
            If e.Key = Key.Enter Then
                Dim searchText As String = SearchField.Text
                PerformSearch(searchText)
            End If
        End Sub

        Friend Sub PerformSearch(searchText As String)
            ' TODO: If multiple searches happen in sequence, optimize by comparing changes.
            SamplesContainer.Children.Clear()

            If Not String.IsNullOrWhiteSpace(searchText) Then
                Dim searchResult = ControlSearch.Search(searchText)
                For Each res In searchResult
                    Dim sampleType As Type = SamplesInfoLoader.GetControlTypeByName(res.Name)
                    If sampleType IsNot Nothing Then
                        Dim controlInstance As Object = Activator.CreateInstance(sampleType)
                        Dim uiElement As UIElement = TryCast(controlInstance, UIElement)
                        If uiElement IsNot Nothing Then
                            SamplesContainer.Children.Add(uiElement)
                        End If
                    End If
                Next
            End If
        End Sub

    End Class
End Namespace
