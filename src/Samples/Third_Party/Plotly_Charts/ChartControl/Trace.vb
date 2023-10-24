Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Trace
        Public Sub New()
            ' Set default values:
            X = New List(Of Object)()
            Y = New List(Of Object)()
            Text = New List(Of String)()
            Mode = TraceMode.Lines
            Hole = 0
        End Sub

        Public Property Name As String

        Public Property X As List(Of Object)

        Public Property Y As List(Of Object)

        Public Property Text As Object

        Public Property Values As List(Of Object)

        Public Property Labels As List(Of String)

        Public Property Type As TraceType

        Public Property Mode As TraceMode

        Public Property Marker As Marker

        Public Property Domain As Domain

        Public Property HoverInfo As String

        Public Property TextInfo As String

        Public Property Hole As Double

        Public Property TextPosition As String

        Public Function ToJavaScriptObject() As Object
            Dim jsX = Interop.ExecuteJavaScript("[]")
            For Each xPoint In X
                Interop.ExecuteJavaScript("$0.push($1)", jsX, Unbox(xPoint))
            Next

            Dim jsY = Interop.ExecuteJavaScript("[]")
            For Each yPoint In Y
                Interop.ExecuteJavaScript("$0.push($1)", jsY, Unbox(yPoint))
            Next

            Dim jsText = Interop.ExecuteJavaScript("[]")
            If TypeOf Text Is List(Of String) Then
                For Each _Text In CType(Text, List(Of String))
                    Interop.ExecuteJavaScript("$0.push($1)", jsText, _Text)
                Next
            ElseIf TypeOf Text Is String AndAlso Not String.IsNullOrEmpty(CStr(Text)) Then
                Interop.ExecuteJavaScript("$0 = $1;", jsText, CStr(Text))
            End If

            Dim jsTrace = Interop.ExecuteJavaScript("new Object()")
            Interop.ExecuteJavaScript("
                      $0['x'] = $1;
                      $0['y'] = $2;
                      $0['type'] = $3;
                      $0['mode'] = $4;
                      if ($5 != '') {
                        $0['name'] = $5;
                      }
                      if ($6.length == 1) {
                        $0['text'] = $6[0];
                      } else if ($6.length > 1) {
                        $0['text'] = $6;
                      }
                ", jsTrace, jsX, jsY, Type.ToString().ToLower(), Mode.ToJavaScriptString(), If(Name, ""), jsText)

            If Marker IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['marker'] = $1;", jsTrace, Marker.ToJavaScriptObject())
            End If

            If Values IsNot Nothing Then
                Dim jsValues = Interop.ExecuteJavaScript("[]")

                For Each _Val In Values
                    Interop.ExecuteJavaScript("$0.push($1);", jsValues, Unbox(_Val))
                Next

                Interop.ExecuteJavaScript("$0['values'] = $1;", jsTrace, jsValues)
            End If

            If Labels IsNot Nothing Then
                Dim jsLabels = Interop.ExecuteJavaScript("[]")

                For Each Lbl In Labels
                    Interop.ExecuteJavaScript("$0.push($1);", jsLabels, Lbl)
                Next

                Interop.ExecuteJavaScript("$0['labels'] = $1;", jsTrace, jsLabels)
            End If

            If Domain IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['domain'] = $1;", jsTrace, Domain.ToJavaScriptObject())
            End If

            If Not String.IsNullOrEmpty(HoverInfo) Then
                Interop.ExecuteJavaScript("$0['hoverinfo'] = $1;", jsTrace, HoverInfo)
            End If

            If Not String.IsNullOrEmpty(TextInfo) Then
                Interop.ExecuteJavaScript("$0['textinfo'] = $1;", jsTrace, TextInfo)
            End If

            Interop.ExecuteJavaScript("$0['hole'] = $1;", jsTrace, Unbox(Hole))

            If Not String.IsNullOrEmpty(TextPosition) Then
                Interop.ExecuteJavaScript("$0['textposition'] = $1;", jsTrace, TextPosition)
            End If

            Return jsTrace
        End Function
    End Class
End Namespace
