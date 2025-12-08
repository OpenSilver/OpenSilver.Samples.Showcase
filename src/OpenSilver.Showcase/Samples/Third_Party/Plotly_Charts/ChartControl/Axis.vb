Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Axis
        Public Sub New()
            ' Set default values:
            Tickangle = 0
            ZeroLine = True
            GrigWidth = 1
        End Sub

        Public Property Title As String

        Public Property Tickangle As Integer

        Public Property ZeroLine As Boolean

        Public Property GrigWidth As Integer

        Public Property TickFont As Font

        Public Property TitleFont As Font

        Public Function ToJavascriptObject() As Object
            Dim jsAxis = Interop.ExecuteJavaScript("[]")

            If Not Equals(Title, Nothing) Then
                Interop.ExecuteJavaScript("$0['title'] = $1;", jsAxis, Title)
            End If

            Interop.ExecuteJavaScript("$0['tickangle'] = $1;", jsAxis, Unbox(Tickangle))

            Interop.ExecuteJavaScript("$0['zeroline'] = $1;", jsAxis, Unbox(ZeroLine))

            Interop.ExecuteJavaScript("$0['grigwidth'] = $1;", jsAxis, Unbox(GrigWidth))

            If TickFont IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['tickfont'] = $1;", jsAxis, TickFont.ToJavaScriptObject())
            End If

            If TitleFont IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['titlefont'] = $1;", jsAxis, TitleFont.ToJavaScriptObject())
            End If

            Return jsAxis
        End Function
    End Class
End Namespace
