Imports OpenSilver.Samples.Showcase.Search
Imports PreviewOnWinRT
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("layout", "window", "popup", "modal", "dialog")>
    Partial Public Class ChildWindow_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub


        Private Sub ButtonTestChildWindow_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim loginWnd As LoginWindow = New LoginWindow()
            AddHandler loginWnd.Closed, New EventHandler(AddressOf loginWnd_Closed)
            loginWnd.Show()
        End Sub

        Private Sub loginWnd_Closed(ByVal sender As Object, ByVal e As EventArgs)
            Dim lw = CType(sender, LoginWindow)
            If lw.DialogResult.HasValue AndAlso lw.DialogResult.Value = True AndAlso Not Equals(lw.nameBox.Text, String.Empty) Then
                Me.TextBlockForTestingChildWindow.Text = "Hello " & lw.nameBox.Text
            Else
                Me.TextBlockForTestingChildWindow.Text = "Login cancelled."
            End If
        End Sub
    End Class
End Namespace
