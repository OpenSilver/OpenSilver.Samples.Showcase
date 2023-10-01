
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

Namespace Global.PreviewOnWinRT
    Partial Public Class SmallChildWindow
        Inherits ChildWindow
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
        End Sub

        Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = False
        End Sub
    End Class
End Namespace
