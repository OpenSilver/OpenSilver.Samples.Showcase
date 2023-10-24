Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public NotInheritable Class ViewSourceButton
        Inherits Button

        Public Sub New()
            Style = TryCast(Application.Current.Resources("ButtonViewSource_Style"), Style)
        End Sub

        Public ReadOnly Property Sources As List(Of ViewSourceButtonInfo) = New List(Of ViewSourceButtonInfo)()

        Protected Overrides Sub OnClick()
            MyBase.OnClick()
            ViewSourceButtonHelper.ViewSource(Sources)
        End Sub
    End Class
End Namespace
