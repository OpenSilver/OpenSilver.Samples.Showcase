Imports OpenSilver.Extensions.FileSystem
Imports Ionic.Zip
Imports System.Collections.Generic
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

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Zip_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Zip_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ZipFile.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/ZipFile.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Zip_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/Zip/Zip_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "ZipFile.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/Zip/ZipFile.vb"
    }
})
        End Sub
    End Class
End Namespace
