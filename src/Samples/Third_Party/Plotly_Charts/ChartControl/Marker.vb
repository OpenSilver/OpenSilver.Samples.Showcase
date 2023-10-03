Imports System.Collections.Generic

Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Marker
        Public Sub New()
            ' Set default values:
            Size = 6
            Opacity = 1.0
        End Sub

        Public Property Size As Integer

        Public Property Color As Object

        Public Property Colors As List(Of String)

        Public Property Opacity As Double

        Public Property Line As Line

        Public Function ToJavaScriptObject() As Object
            Dim jsMarker = Interop.ExecuteJavaScript("new Object()")

            Interop.ExecuteJavaScript("
                      $0['size'] = $1;
                ", jsMarker, Unbox(Size))

            If TypeOf Color Is String AndAlso Not String.IsNullOrEmpty(CStr(Color)) Then
                Interop.ExecuteJavaScript("$0['color'] = $1;", jsMarker, Color)
            ElseIf TypeOf Color Is List(Of String) AndAlso Color IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['color'] = [];", jsMarker)

                For Each _Color In CType(Me.Color, List(Of String))
                    Interop.ExecuteJavaScript("$0['color'].push($1);", jsMarker, _Color)
                Next
            End If

            Interop.ExecuteJavaScript("$0['opacity'] = $1;", jsMarker, Unbox(Opacity))

            If Line IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['line'] = $1;", jsMarker, Line.ToJavaScriptObject())
            End If

            If Colors IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['colors'] = [];", jsMarker)

                For Each _Color In Colors
                    Interop.ExecuteJavaScript("$0['colors'].push($1);", jsMarker, _Color)
                Next
            End If

            Return jsMarker
        End Function
    End Class
End Namespace
