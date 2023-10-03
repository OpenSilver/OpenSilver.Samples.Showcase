Imports System

Namespace Global.DotNetForHtml5.Showcase.SampleRestWebService.Models
    Public Class ToDoItem
        Public Sub New()
        End Sub

        Public Sub New(ByVal description As String, ByVal ownerId As Integer)
            Me.Description = description
        End Sub

        Public Property Id As Guid
        Public Property OwnerId As Guid
        Public Property Description As String
    End Class
End Namespace
