Imports System
Imports System.IO
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    Partial Public Class OpenFileDialog_Demo
        Inherits UserControl

        Private openFileDialog1 As Controls.OpenFileDialog

        Public Sub New()
            Me.InitializeComponent()
            openFileDialog1 = New Controls.OpenFileDialog() With {
                .Filter = "Text files (*.txt)|*.txt"
            }
        End Sub

        Private Async Sub ButtonOpenFile_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Await openFileDialog1.ShowDialogAsync() = True Then

                Try
                    Dim file = openFileDialog1.File
                    FileNameTextBlock.Text = file.Name

                    Using reader As StreamReader = New StreamReader(file.OpenRead(), Encoding.UTF8)
                        MessageBox.Show(reader.ReadToEnd())
                    End Using

                Catch ex As Exception
                    'fail silently
                End Try
            End If
        End Sub
    End Class
End Namespace
