﻿Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("asynchronous", "await", "task", "thread")>
    Partial Public Class AsyncAwait_Demo
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
    End Class
End Namespace
