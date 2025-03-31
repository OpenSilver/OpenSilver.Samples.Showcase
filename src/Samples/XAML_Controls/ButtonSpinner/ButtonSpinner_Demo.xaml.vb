Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("updown")>
    Partial Public Class ButtonSpinner_Demo
        Inherits ContentControl

        Private ReadOnly _months As String() = {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        }

        Private _monthIndex As Integer = 0

        Public Sub New()
            InitializeComponent()
            UpdateSpinnerContent()
        End Sub

        Private Sub OnButtonSpinnerSpin(sender As Object, e As SpinEventArgs)
            If e.Direction = SpinDirection.Increase Then
                _monthIndex += 1
            Else
                _monthIndex -= 1
            End If

            If _monthIndex < 0 Then
                _monthIndex = _months.Length - 1
            ElseIf _monthIndex >= _months.Length Then
                _monthIndex = 0
            End If

            UpdateSpinnerContent()
        End Sub

        Private Sub UpdateSpinnerContent()
            spinner.Content = _months(_monthIndex)
        End Sub

    End Class

End Namespace
