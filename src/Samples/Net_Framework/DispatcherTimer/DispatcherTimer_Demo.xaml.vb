Imports System
Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Threading
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
    Public Partial Class DispatcherTimer_Demo
        Inherits UserControl
        Private _dispatcherTimer As DispatcherTimer

        Public Sub New()
            Me.InitializeComponent()


            ' Initialize the DispatcherTimer
            _dispatcherTimer = New DispatcherTimer() With {
                .Interval = New TimeSpan(0, 0, 0, 0, 100)
            }
            AddHandler _dispatcherTimer.Tick, AddressOf DispatcherTimer_Tick
        End Sub

        Private Sub ButtonToStartTimer_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            _dispatcherTimer.Start()
        End Sub

        Private Sub ButtonToStopTimer_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            _dispatcherTimer.Stop()
        End Sub

        Private Sub DispatcherTimer_Tick(ByVal sender As Object, ByVal e As Object)
            ' Increment the counter by 1
            If Equals(Me.CounterTextBlock.Text, Nothing) OrElse Equals(Me.CounterTextBlock.Text, String.Empty) Then
                Me.CounterTextBlock.Text = "0"
            Else
                Me.CounterTextBlock.Text = (Integer.Parse(CStr(Me.CounterTextBlock.Text)) + 1).ToString()
            End If
        End Sub

        Public Sub Dispose()
            _dispatcherTimer.Stop()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DispatcherTimer_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DispatcherTimer_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DispatcherTimer_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
