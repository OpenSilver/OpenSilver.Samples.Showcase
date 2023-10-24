Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class DateAndTimePicker_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub RegisterEvent_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Me.TimePicker.Value IsNot Nothing AndAlso Me.DatePicker.SelectedDate IsNot Nothing Then
                Call MessageBox.Show("Your event is confirmed at " & Me.TimePicker.Value.Value.ToShortTimeString() & " on " & Me.DatePicker.SelectedDate.Value.ToShortDateString())
            Else
                MessageBox.Show("Please enter a Date and a Time")
            End If
        End Sub
    End Class
End Namespace
