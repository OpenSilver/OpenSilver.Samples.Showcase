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
    Partial Public Class RadioButton_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub RadioButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dispatcher.BeginInvoke(Sub() MessageBox.Show(If(Me.RadioButton1.IsChecked = True, "Option 1 selected", "Option 2 selected")))
        End Sub
    End Class
End Namespace
