Imports System.Collections.Generic

Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Domain
        Public Property X As List(Of Double)

        Public Property Y As List(Of Double)

        Public Function ToJavaScriptObject() As Object
            Dim jsDomain = Interop.ExecuteJavaScript("[]")

            If X IsNot Nothing Then
                Dim jsX = Interop.ExecuteJavaScript("[]")

                For Each d In X
                    Interop.ExecuteJavaScript("$0.push($1);", jsX, Unbox(d))
                Next
                Interop.ExecuteJavaScript("$0['x'] = $1;", jsDomain, jsX)
            End If

            If Y IsNot Nothing Then
                Dim jsY = Interop.ExecuteJavaScript("[]")

                For Each d In Y
                    Interop.ExecuteJavaScript("$0.push($1);", jsY, Unbox(d))
                Next
                Interop.ExecuteJavaScript("$0['y'] = $1;", jsDomain, jsY)
            End If

            Return jsDomain
        End Function
    End Class
End Namespace
