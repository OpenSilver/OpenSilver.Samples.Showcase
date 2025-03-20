Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("hyperlink", "text", "navigation", "control", "button", "web")>
    Partial Public Class HyperlinkButton_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If OPENSILVER Then
            Me.HyperlinkButtonDemo.NavigateUri = New Uri("http://www.opensilver.net")
#End If

        End Sub
    End Class
End Namespace
