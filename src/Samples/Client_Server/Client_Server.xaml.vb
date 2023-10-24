Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Client_Server
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If OPENSILVER
            'REST_WebClientDemo.Visibility = System.Windows.Visibility.Collapsed;
#End If
        End Sub
    End Class
End Namespace
