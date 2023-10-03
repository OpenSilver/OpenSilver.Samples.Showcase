
Namespace Global.Newtonsoft.Json
    Public Module InteropHelper
#If Not BRIDGE Then
        <JSIL.Meta.JSReplacement("$value")>
        Public Function Unbox(ByVal value As Object) As Object
#Else
        <Bridge.Template("({value}.v != undefined ? {value}.v : {value})")>
        Public Function Unbox(ByVal value As Object) As Object
#End If

            Return value
        End Function
    End Module
End Namespace
