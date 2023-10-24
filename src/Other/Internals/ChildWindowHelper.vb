Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Friend Module ChildWindowHelper
        Public Sub ShowChildWindow(ByVal content As UIElement)
            Dim childWindow = New ChildWindow() With {
    .Content = content,
    .Style = CType(Application.Current.Resources("ViewMoreChildWindow_Style"), Style)
}
            childWindow.Show()
        End Sub
    End Module
End Namespace
