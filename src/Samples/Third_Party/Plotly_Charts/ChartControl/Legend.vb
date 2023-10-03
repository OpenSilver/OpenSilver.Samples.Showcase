
Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Legend
        Public Sub New()
            ' Set default values:
            X = 0
            Y = 0
        End Sub

        Public Property X As Double

        Public Property Y As Double

        Public Property BgColor As String

        Public Property BorderColor As String

        Public Function ToJavaScriptObject() As Object
            Dim jsLegend = Interop.ExecuteJavaScript("[]")

            Interop.ExecuteJavaScript("$0['x'] = $1;", jsLegend, Unbox(X))
            Interop.ExecuteJavaScript("$0['y'] = $1;", jsLegend, Unbox(Y))

            If Not String.IsNullOrEmpty(BgColor) Then
                Interop.ExecuteJavaScript("$0['bgcolor'] = $1;", jsLegend, BgColor)
            End If

            If Not String.IsNullOrEmpty(BorderColor) Then
                Interop.ExecuteJavaScript("$0['bordercolor'] = $1;", jsLegend, BorderColor)
            End If

            Return jsLegend
        End Function
    End Class
End Namespace
