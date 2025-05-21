Namespace OpenSilver.Samples.Showcase
    Friend Module FileLoader
        Public Async Function TryLoadJavaScriptFile(url As String) As Task(Of Boolean)
            Try
                Await Interop.LoadJavaScriptFile(url)
                Return True
            Catch ex As Exception
                Console.WriteLine(ex)
                Return False
            End Try
        End Function

        Public Async Function TryLoadCssFile(url As String) As Task(Of Boolean)
            Try
                Await Interop.LoadCssFile(url)
                Return True
            Catch ex As Exception
                Console.WriteLine(ex)
                Return False
            End Try
        End Function
    End Module
End Namespace
