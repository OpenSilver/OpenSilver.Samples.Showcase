Imports System.Collections.Generic
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
    Partial Public Class FullScreen_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Public Sub EnterFullScreen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Application.Current.Host.Content.IsFullScreen = True
        End Sub

        Public Sub ExitFullSCreen_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Application.Current.Host.Content.IsFullScreen = False
        End Sub
    End Class
End Namespace
