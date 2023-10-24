Imports System
Imports System.Collections.Generic

#If SLMIGRATION Then
Imports System.Windows
Imports System.Windows.Controls
#Else
Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls
#End If

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
