Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Font
        Public Sub New()
            ' Set default values:
            Size = 14
        End Sub

        Public Property Family As String

        Public Property Size As Integer

        Public Property Color As String

        Public Function ToJavaScriptObject() As Object
            Dim jsFont = Interop.ExecuteJavaScript("[]")

            If Not String.IsNullOrEmpty(Family) Then
                Interop.ExecuteJavaScript("$0['family'] = $1;", jsFont, Family)
            End If

            Interop.ExecuteJavaScript("$0['size'] = $1;", jsFont, Unbox(Size))

            If Not String.IsNullOrEmpty(Color) Then
                Interop.ExecuteJavaScript("$0['color'] = $1;", jsFont, Color)
            End If

            Return jsFont
        End Function
    End Class
End Namespace
