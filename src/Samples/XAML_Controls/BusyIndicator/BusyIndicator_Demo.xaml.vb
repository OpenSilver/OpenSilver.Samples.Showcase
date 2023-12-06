Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Threading

Namespace OpenSilver.Samples.Showcase
    Partial Public Class BusyIndicator_Demo
        Inherits UserControl

        Private timer As DispatcherTimer

        Public Sub New()
            Me.InitializeComponent()
            timer = New DispatcherTimer() With {
                .Interval = New TimeSpan(0, 0, 3)
            }
            AddHandler timer.Tick, AddressOf StopBusyIndicator
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MyBusyIndicator.IsBusy = True
            timer.Start()
        End Sub

        Private Sub StopBusyIndicator(ByVal sender As Object, ByVal e As EventArgs)
            timer.[Stop]()
            MyBusyIndicator.IsBusy = False
        End Sub
    End Class
End Namespace
