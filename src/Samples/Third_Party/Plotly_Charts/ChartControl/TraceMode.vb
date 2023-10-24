Imports System.Runtime.CompilerServices

Namespace Global.OpenSilver.Extensions.Plotly
    <Flags>
    Public Enum TraceMode As Short
        None = 0
        Lines = 1
        Markers = 2
        Text = 4
    End Enum

    Friend Module TraceModeExtensionMethods
        <Extension()>
        Public Function ToJavaScriptString(ByVal traceMode As TraceMode) As String
            If traceMode = 0 Then
                Return "none"
            Else
                Dim result As List(Of String) = New List(Of String)()
                For Each value As TraceMode In [Enum].GetValues(GetType(TraceMode))
                    If (traceMode And value) <> 0 Then result.Add(value.ToString())
                Next
                Return String.Join("+", result).ToLower()
            End If
        End Function
    End Module
End Namespace
