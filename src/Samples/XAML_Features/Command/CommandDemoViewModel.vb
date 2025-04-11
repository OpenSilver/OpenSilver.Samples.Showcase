Imports System.Windows

Namespace OpenSilver.Samples.Showcase

    Public Class CommandDemoViewModel
        Inherits ObservableObject

        Public ReadOnly Property ShowMessageCommand As IRelayCommand

        Private _message As String = "Message"
        Public Property Message As String
            Get
                Return _message
            End Get
            Set(value As String)
                If SetProperty(_message, value) Then
                    ShowMessageCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        Public Sub New()
            ShowMessageCommand = New RelayCommand(Of String)(AddressOf ShowMessage, Function(s) Not String.IsNullOrWhiteSpace(s))
        End Sub

        Private Sub ShowMessage(message As String)
            MessageBox.Show($"Command is executed: {message}")
        End Sub

    End Class

End Namespace
