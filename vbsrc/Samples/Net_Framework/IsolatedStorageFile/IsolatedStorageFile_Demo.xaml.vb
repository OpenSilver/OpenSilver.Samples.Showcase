Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.IsolatedStorage
Imports System.Text
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Partial Class IsolatedStorageFile_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonSaveToIsolatedStorageFile_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() Then
                Dim fileName = "SampleFile.txt"
                Dim data As String = Me.TextBoxFileStorageDemo.Text

                Using storage As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly()
                    Dim fs As IsolatedStorageFileStream = Nothing
                    Using CSharpImpl.__Assign(fs, storage.CreateFile(fileName))
                        If fs IsNot Nothing Then
                            Dim encoding As Encoding = New UTF8Encoding()
                            Dim bytes = encoding.GetBytes(data)
                            fs.Write(bytes, 0, bytes.Length)
                            fs.Close()
                            MessageBox.Show("A new file named SampleFile.txt was successfully saved to the storage.")
                        Else
                            MessageBox.Show("Unable to save the file SampleFile.txt to the storage.")
                        End If
                    End Using
                End Using
            End If
        End Sub

        Private Sub ButtonLoadFromIsolatedStorageFile_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() Then
                Dim fileName = "SampleFile.txt"

                Using storage As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForAssembly()
                    If storage.FileExists(fileName) Then
                        Using fs = storage.OpenFile(fileName, FileMode.Open)
                            If fs IsNot Nothing Then
                                Using sr As StreamReader = New StreamReader(fs)
                                    Dim data As String = sr.ReadToEnd()
                                    MessageBox.Show("The following text was read from the file SampleFile.txt located in the storage: " & data)
                                End Using
                            Else
                                MessageBox.Show("Unable to load the file SampleFile.txt from the storage.")
                            End If
                        End Using
                    Else
                        MessageBox.Show("No file named SampleFile.txt was found in the storage.")
                    End If
                End Using
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

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "IsolatedStorageFile_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/IsolatedStorageFile/IsolatedStorageFile_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "IsolatedStorageFile_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/IsolatedStorageFile/IsolatedStorageFile_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "IsolatedStorageFile_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/IsolatedStorageFile/IsolatedStorageFile_Demo.xaml.vb"
    }
})
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class

    End Class
End Namespace
