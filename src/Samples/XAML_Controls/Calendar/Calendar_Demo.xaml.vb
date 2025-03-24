Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("input", "calendar", "date", "selection", "schedule", "picker", "control")>
    Partial Public Class Calendar_Demo
        Inherits UserControl
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnPastDatesChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If sampleCalendar Is Nothing Then
                Return
            End If

            If chkPastDateSelection.IsChecked Then
                sampleCalendar.BlackoutDates.Clear()
            Else
                Try
                    sampleCalendar.BlackoutDates.AddDatesInPast()
                Catch
                    chkPastDateSelection.IsChecked = True
                End Try
            End If
        End Sub
    End Class
End Namespace