Imports System.Collections.Generic
Imports System.Threading.Tasks
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

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Partial Class AsyncAwait_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub ButtonToDemonstrateAsyncAwait_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.Visibility = Visibility.Collapsed
            Me.TaskBasedCounterTextBlock.Visibility = Visibility.Visible
            Me.TaskBasedCounterTextBlock.Text = "5 seconds left"
            Await Task.Delay(1000)
            Me.TaskBasedCounterTextBlock.Text = "4 seconds left"
            Await Task.Delay(1000)
            Me.TaskBasedCounterTextBlock.Text = "3 seconds left"
            Await Task.Delay(1000)
            Me.TaskBasedCounterTextBlock.Text = "2 seconds left"
            Await Task.Delay(1000)
            Me.TaskBasedCounterTextBlock.Text = "1 second left"
            Await Task.Delay(1000)
            Me.TaskBasedCounterTextBlock.Text = "Done!"
            Me.TaskBasedCounterTextBlock.Visibility = Visibility.Collapsed
            button.Visibility = Visibility.Visible
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "AsyncAwait_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "AsyncAwait_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "AsyncAwait_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
