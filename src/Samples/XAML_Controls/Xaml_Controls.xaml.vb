Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Xaml_Controls
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If OPENSILVER
            Me.NonModalChildWindow.Visibility = Visibility.Collapsed
#End If
            Me.ScrollBarDemo.Visibility = Visibility.Collapsed
            Me.ThumbDemo.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
