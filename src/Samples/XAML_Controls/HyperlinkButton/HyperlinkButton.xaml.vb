Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
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
