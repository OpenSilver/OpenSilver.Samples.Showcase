Imports System.Windows

Namespace OpenSilver.Samples.Showcase
    Public NotInheritable Class UIElementHelpers
        Public Shared Function WaitForLoadedAsync(element As FrameworkElement) As Task
            If element.IsLoaded Then
                Return Task.CompletedTask
            End If

            Dim tcs As New TaskCompletionSource(Of Object)()
            Dim handler As RoutedEventHandler = Nothing

            handler = Sub(s, e)
                          RemoveHandler element.Loaded, handler
                          tcs.SetResult(Nothing)
                      End Sub

            AddHandler element.Loaded, handler
            Return tcs.Task
        End Function
    End Class
End Namespace
