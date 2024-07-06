Imports System.IO.IsolatedStorage
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class IsolatedStorageSettings_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonSaveToIsolatedStorageSettings_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() Then
                Dim key = "SampleKey"
                Dim value As String = Me.TextBoxIsolatedStorageSettingsDemo.Text

                IsolatedStorageSettings.ApplicationSettings(key) = value
                MessageBox.Show("The text was successfully saved to the storage.")
            End If
        End Sub

        Private Sub ButtonLoadFromIsolatedStorageSettings_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() Then
                Dim key = "SampleKey"

                Dim value As String
                If IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, value) Then
                    MessageBox.Show("The following text was read from the storage: " & value)
                Else
                    MessageBox.Show("No text was found in the storage.")
                End If
            End If
        End Sub

        Private Sub ButtonDeleteFromIsolatedStorageSettings_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() Then
                Dim key = "SampleKey"
                IsolatedStorageSettings.ApplicationSettings.Remove(key)
                MessageBox.Show("The text was successfully removed from the storage.")
            End If
        End Sub

        Private Function DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() As Boolean
            Return False
        End Function
    End Class
End Namespace
