
#If SLMIGRATION
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.VBShowcase
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
