Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Welcome
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
#If OPENSILVER
            Me.IntroductionTextBlock.Text = "This app was written in standard C# and XAML, and compiled to WebAssembly using OpenSilver."
#End If
        End Sub
    End Class
End Namespace
