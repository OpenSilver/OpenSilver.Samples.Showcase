Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("console", "output", "debugging", "logging")>
    Partial Public Class Console_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnWriteToConsoleButtonClick(sender As Object, e As RoutedEventArgs)
            Console.WriteLine($"Console test message {DateTime.Now}")
        End Sub

        Private Sub OnWriteToDebugButtonClick(sender As Object, e As RoutedEventArgs)
            Debug.WriteLine($"Debug test message {DateTime.Now}")
        End Sub

    End Class

End Namespace
