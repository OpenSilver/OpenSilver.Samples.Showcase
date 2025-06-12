Imports System.Collections.ObjectModel
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    Public Class PageCategoryInfo
        Implements IMenuElement

        Public Property Name As String
        Public Property Foreground As Brush
        Public Property Pages As New ObservableCollection(Of PageInfo)
    End Class
End Namespace
