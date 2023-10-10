Imports System.Collections.ObjectModel
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
    Public Partial Class SampleMaterialChildWindow
        Inherits ChildWindow
        Public Sub New()
            Me.InitializeComponent()

            Dim items As ObservableCollection(Of String) = New ObservableCollection(Of String)() From {
                "Item 1",
                "Item 2",
                "Item 3"
            }
            DataContext = items
        End Sub

        Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
        End Sub

        Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
        End Sub
    End Class
End Namespace
