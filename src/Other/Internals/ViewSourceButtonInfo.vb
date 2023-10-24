
Namespace Global.OpenSilver.Samples.Showcase
    Public Class ViewSourceButtonInfo
        Public Sub New()
        End Sub

        Public Sub New(relativePath As String, fileName As String)
            Me.RelativePath = relativePath
            Me.FileName = fileName
        End Sub

        Public Property FileName As String

        Public Property RelativePath As String

        Public Property Branch As String = "develop"

        Public Property Repository As String = "OpenSilver.Samples.Showcase"

        Public Property Owner As String = "OpenSilver"

        Public Property TabHeader As String

        Public Function GetHeader() As String
            Return If(Not String.IsNullOrEmpty(TabHeader), TabHeader, FileName)
        End Function

        Public Function GetAbsoluteUrl() As String
            Return $"https://github.com/{Owner}/{Repository}/blob/{Branch}/{RelativePath}/{FileName}"
        End Function
    End Class
End Namespace
