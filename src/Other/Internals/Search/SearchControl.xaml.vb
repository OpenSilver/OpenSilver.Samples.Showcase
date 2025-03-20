Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace OpenSilver.Samples.Showcase
    Partial Public Class SearchControl
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Sub ButtonSearch_Click(sender As Object, e As RoutedEventArgs)
            StartSearch(SearchField.Text)
        End Sub

        Private Sub SearchField_KeyDown(sender As Object, e As KeyEventArgs)
            If e.Key = Key.Enter Then
                StartSearch(SearchField.Text)
            End If
        End Sub

        Public Sub StartSearch(searchTerms As String)
            If Not String.IsNullOrWhiteSpace(searchTerms) Then
                ' Get the MainPage and start the search:
                Dim mainPage As MainPage = TryCast(Application.Current.RootVisual, MainPage)
                If mainPage IsNot Nothing Then
                    mainPage.StartSearch(searchTerms)
                End If
            End If
        End Sub
    End Class
End Namespace
