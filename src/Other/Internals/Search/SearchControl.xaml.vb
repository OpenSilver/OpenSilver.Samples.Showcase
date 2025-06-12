Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace OpenSilver.Samples.Showcase
    Partial Public Class SearchControl
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Async Sub ButtonSearch_Click(sender As Object, e As RoutedEventArgs)
            Await StartSearch(SearchField.Text)
            SearchField.Focus()
        End Sub

        Private Async Sub SearchField_KeyDown(sender As Object, e As KeyEventArgs)
            If e.Key = Key.Enter Then
                Await StartSearch(SearchField.Text)
            End If
        End Sub

        Public Async Function StartSearch(searchTerms As String) As Task
            If Not String.IsNullOrWhiteSpace(searchTerms) Then
                ' Get the MainPage and start the search:
                Dim mainPage As MainPage = TryCast(Application.Current.RootVisual, MainPage)
                If mainPage IsNot Nothing Then
                    Await mainPage.StartSearch(searchTerms)
                End If
            End If
        End Function
    End Class
End Namespace
