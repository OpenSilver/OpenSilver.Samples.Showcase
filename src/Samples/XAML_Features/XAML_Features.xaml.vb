
#If SLMIGRATION
Imports System.Windows
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

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Xaml_Features
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
#If OPENSILVER
            'Binding1Demo.Visibility = Visibility.Collapsed;
#End If
            Me.MarkupExtensionsDemo.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
