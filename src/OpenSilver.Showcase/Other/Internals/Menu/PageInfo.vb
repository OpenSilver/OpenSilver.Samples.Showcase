Imports System.Windows.Media

Namespace OpenSilver.Showcase
    Public Class PageInfo
        Implements IMenuElement

        Public Property Name As String
        Public Property Path As String
        Public Property Icon As String
        Public Property IconBrush As Brush
        Public Property IsVisibleInMenu As Boolean = True
    End Class
End Namespace
