Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Data
        Public Sub New()
            ' Set default values:
            Traces = New List(Of Trace)()
        End Sub

        Public Property Traces As List(Of Trace)

        Public Function ToJavaScriptObject() As Object
            ' Create the JS array:
            Dim jsArray = Interop.ExecuteJavaScript("[]")

            For Each trace In Traces
                Dim jsTrace = trace.ToJavaScriptObject()
                Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsTrace)
            Next

            Return jsArray
        End Function
    End Class
End Namespace
