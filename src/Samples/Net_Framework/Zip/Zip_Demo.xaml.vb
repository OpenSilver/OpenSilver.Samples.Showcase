Imports OpenSilver.Samples.Showcase.Search
Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("compression", "file", "archive", "savefiledialog")>
    Partial Public Class Zip_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Async Sub ButtonGenerateZip_Click(sender As Object, e As RoutedEventArgs)
            Using memoryStream As New MemoryStream()
                Using archive As New ZipArchive(memoryStream, ZipArchiveMode.Create)
                    Dim zipEntry = archive.CreateEntry("SampleText.txt", CompressionLevel.Optimal)
                    Using entryStream = zipEntry.Open()
                        Using writer As New StreamWriter(entryStream)
                            writer.Write("Hello World!")
                        End Using
                    End Using
                End Using

                Dim dialog As New Controls.SaveFileDialog With {
                    .DefaultExt = ".zip",
                    .Filter = "Zip files (*.zip)|*.zip|All files (*.*)|*.*",
                    .DefaultFileName = "MyTestFile"
                }

                If Await dialog.ShowDialogAsync() = True Then
                    Dim data = memoryStream.ToArray()
                    Using saveFileStream = Await dialog.OpenFileAsync()
                        Await saveFileStream.WriteAsync(data, 0, data.Length)
                        Await saveFileStream.FlushAsync()
                    End Using
                End If
            End Using
        End Sub

    End Class

End Namespace
