Namespace OpenSilver.Samples.Showcase.Search
    <AttributeUsage(AttributeTargets.[Class], AllowMultiple:=False)>
    Public Class SearchKeywordsAttribute
        Inherits Attribute

        Public ReadOnly Property Keywords As String()

        Public Sub New(ParamArray keywords As String())
            Me.Keywords = keywords
        End Sub
    End Class
End Namespace
