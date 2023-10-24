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
            '-----------------------------------------------------------
            ' When running inside Internet Explorer or Edge, the HTML5
            ' Storage API is available only if the URL starts with http
            ' or https. This method will display a message to the user
            ' to inform her about this.
            '-----------------------------------------------------------
            If CSharpXamlForHtml5.Environment.IsRunningInJavaScript Then
                'Execute a piece of JavaScript code:
                If IsRunningFromLocalFileSystemOnInternetExplorer() Then
                    MessageBox.Show("The local storage - used to persist data - is not available on Internet Explorer or Edge when running the website from the local file system (ie. the URL starts with 'c:' or 'file:///'). To solve the problem, please run the website from a web server instead (ie. the URL must start with 'http://' or 'https://') or test the local storage using a different browser.")
                    Return True
                End If
            End If
            Return False
        End Function

        Private Function IsRunningFromLocalFileSystemOnInternetExplorer() As Boolean
            Return Convert.ToBoolean(Interop.ExecuteJavaScript("window.IE_VERSION && document.location.protocol === ""file:"""))
        End Function
    End Class
End Namespace
