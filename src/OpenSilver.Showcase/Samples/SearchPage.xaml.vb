Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Navigation
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase
    Partial Public Class SearchPage
        Inherits Page

        Private Const SearchArgName As String = "SearchTerms"

        Public Sub New()
            InitializeComponent()

            'AddHandler SearchField.Loaded, AddressOf OnSearchFieldLoaded
            'SearchField.AddHandler(KeyDownEvent, New KeyEventHandler(AddressOf SearchField_KeyDown), True)
        End Sub

        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            Dim searchTerms As String = Nothing
            If NavigationContext.QueryString.TryGetValue(SearchArgName, searchTerms) AndAlso Not String.IsNullOrWhiteSpace(searchTerms) Then
                'SearchField.Text = searchTerms
                PerformSearch(searchTerms)
            End If
        End Sub

        'Private Sub OnSearchFieldLoaded(sender As Object, e As RoutedEventArgs)
        '    SearchField.Focus()
        'End Sub

        'Private Sub ButtonSearch_Click(sender As Object, e As RoutedEventArgs)
        '    NavigateToSearch()
        'End Sub

        'Private Sub SearchField_KeyDown(sender As Object, e As KeyEventArgs)
        '    If e.Key = Key.Enter Then
        '        NavigateToSearch()
        '    End If
        'End Sub

        'Private Sub NavigateToSearch()
        '    Dim searchText As String = SearchField.Text
        '    NavigationService.Navigate(New Uri($"/Search/{searchText}", UriKind.Relative))
        'End Sub

        Friend Sub PerformSearch(searchText As String)
            'todo: if multiple searches one after the other, increase efficiency by only looking at the changes between the current search and the previous search
            'for now, we just clear everything.
            SamplesPanel.Items.Clear()

            If Not String.IsNullOrWhiteSpace(searchText) Then
                Dim searchResult = ControlSearch.Search(searchText)
                For Each res In searchResult
                    Dim sampleType As Type = SamplesInfoLoader.GetControlTypeByName(res.Name)
                    If sampleType IsNot Nothing Then
                        Dim controlInstance As Object = Activator.CreateInstance(sampleType)

                        If TypeOf controlInstance Is UIElement Then
                            SamplesPanel.Items.Add(CType(controlInstance, UIElement))
                        End If
                    End If
                Next
            End If
        End Sub
    End Class
End Namespace
