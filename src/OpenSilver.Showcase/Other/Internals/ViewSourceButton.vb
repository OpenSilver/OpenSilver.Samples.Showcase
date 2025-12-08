Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Showcase
    Public NotInheritable Class ViewSourceButton
        Inherits Button

        Public Sub New()
            Style = TryCast(Application.Current.Resources("ButtonViewSource_Style"), Style)
        End Sub

        Public ReadOnly Property Sources As List(Of ViewSourceButtonInfo) = New List(Of ViewSourceButtonInfo)()

        Protected Overrides Sub OnClick()
            MyBase.OnClick()
            ViewSource(Sources)
        End Sub

        Private Shared Sub ViewSource(ByVal sourcePaths As ICollection(Of ViewSourceButtonInfo))
            If sourcePaths Is Nothing OrElse sourcePaths.Count = 0 Then
                Return
            End If

            Dim panel = New ViewSourcePanel()
            panel.ViewSource(sourcePaths)
            MainPage.Current.ViewSourceCode(panel)
        End Sub
    End Class
End Namespace
