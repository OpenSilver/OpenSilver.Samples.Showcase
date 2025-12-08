Imports System.Windows
Imports System.Windows.Controls
Imports Ionic.Zip
Imports OpenSilver.Extensions.FileSystem
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("compression", "jszip", "file", "archive", "interop", "ionic")>
    Partial Public Class JSZip_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Async Sub ButtonGenerateZip_Click(sender As Object, e As RoutedEventArgs)
            Dim zipFile As New ZipFile()
            Await zipFile.AddFile("SampleText.txt", "Hello World!")
            Dim jsBlob = Await zipFile.SaveToJavaScriptBlob()

            If jsBlob IsNot Nothing Then
                Await FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip")
            End If
        End Sub

    End Class

End Namespace
