Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Annotation
        Public Sub New()
            ' Set default values:
            ShowArrow = True
        End Sub

        Public Property X As Object

        Public Property Y As Object

        Public Property Text As String

        Public Property Xanchor As String

        Public Property Yanchor As String

        Public Property ShowArrow As Boolean

        Public Property Font As Font

        Public Function ToJavaScriptObject() As Object
            Dim jsAnnotation = Interop.ExecuteJavaScript("[]")

            If X IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['x'] = $1;", jsAnnotation, Unbox(X))
            End If

            If Y IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['y'] = $1;", jsAnnotation, Unbox(Y))
            End If

            If Not String.IsNullOrEmpty(Text) Then
                Interop.ExecuteJavaScript("$0['text'] = $1;", jsAnnotation, Text)
            End If

            If Not String.IsNullOrEmpty(Xanchor) Then
                Interop.ExecuteJavaScript("$0['xanchor'] = $1;", jsAnnotation, Xanchor)
            End If

            If Not String.IsNullOrEmpty(Yanchor) Then
                Interop.ExecuteJavaScript("$0['yanchor'] = $1;", jsAnnotation, Yanchor)
            End If

            Interop.ExecuteJavaScript("$0['showarrow'] = $1;", jsAnnotation, Unbox(ShowArrow))

            If Font IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['font'] = $1;", jsAnnotation, Font.ToJavaScriptObject())
            End If

            Return jsAnnotation
        End Function
    End Class
End Namespace
