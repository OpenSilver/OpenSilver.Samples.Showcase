Imports System.IO
Imports System.IO.IsolatedStorage
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
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
            Return False
        End Function

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class

    End Class
End Namespace
