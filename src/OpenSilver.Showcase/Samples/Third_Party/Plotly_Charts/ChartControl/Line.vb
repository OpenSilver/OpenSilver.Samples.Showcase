Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Line
        Public Sub New()
            ' Set default values:
            Width = 0.0
        End Sub

        Public Property Color As String

        Public Property Width As Double

        Public Function ToJavaScriptObject() As Object
            Dim jsLine = Interop.ExecuteJavaScript("[]")

            If Not String.IsNullOrEmpty(Color) Then
                Interop.ExecuteJavaScript("$0['color'] = $1;", jsLine, Color)
            End If

            Interop.ExecuteJavaScript("$0['width'] = $1;", jsLine, Unbox(Width))

            Return jsLine
        End Function
    End Class
End Namespace
