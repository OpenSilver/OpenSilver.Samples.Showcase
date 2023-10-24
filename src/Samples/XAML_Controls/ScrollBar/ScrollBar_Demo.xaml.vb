Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
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
    Partial Public Class ScrollBar_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.TextDisplay.Text = Me.Scrollbar.Value.ToString("0.000")
        End Sub

        Private Sub ScrollBar_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
            Me.TextDisplay.Text = e.NewValue.ToString("0.000")
        End Sub

    End Class
End Namespace
