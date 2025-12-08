Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("error handling", "exception", "debugging", "logging")>
    Partial Public Class UnhandledException_Demo
        Inherits UserControl

        Private _exceptionsReceived As Integer = 0

        Shared Sub New()
            AddHandler Application.Current.UnhandledException, AddressOf OnUnhandledException
        End Sub

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonThrowException_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Throw New Exception("This exception was thrown outside of a Try/Catch statement and handled using UnhandledException")
        End Sub

        Private Shared Sub OnUnhandledException(ByVal sender As Object, ByVal e As ApplicationUnhandledExceptionEventArgs)
            Dim exceptionStackMessages As String = "Received an unhandled Exception: "
            Dim ex As Exception = e.ExceptionObject
            Dim spacing As String = "  "

            While ex IsNot Nothing
                exceptionStackMessages += Environment.NewLine & spacing & "-" & ex.[GetType]().Name & ": " & ex.Message
                spacing += "  "
                ex = ex.InnerException
            End While

            MessageBox.Show(exceptionStackMessages)
        End Sub
    End Class
End Namespace
