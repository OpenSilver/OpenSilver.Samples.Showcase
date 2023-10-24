Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Threading

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class DispatcherTimer_Demo
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
    End Class
End Namespace
