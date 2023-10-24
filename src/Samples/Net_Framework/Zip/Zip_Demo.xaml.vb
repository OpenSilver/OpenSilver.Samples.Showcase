Imports OpenSilver.Extensions.FileSystem
Imports Ionic.Zip
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Zip_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub ButtonGenerateZip_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Using zipFile As ZipFile = New ZipFile()
                Await zipFile.AddFile("SampleText.txt", "Hello World!")
                Dim jsBlob = Await zipFile.SaveToJavaScriptBlob()
                Await FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip")
            End Using
        End Sub

        'async void OnFileOpened(object sender, OpenSilver.Extensions.FileOpenDialog.FileOpenedEventArgs e)
        '{
        '    var javaScriptBlob = e.JavaScriptBlob;
        '    ZipFile zipFile = await ZipFile.Read(javaScriptBlob);
        '    ZipEntry entry = zipFile["MyTestFileInsideTheZIP.txt"];
        '    string content = entry.ExtractToString();
        '    MessageBox.Show(content);
        '}
    End Class
End Namespace
