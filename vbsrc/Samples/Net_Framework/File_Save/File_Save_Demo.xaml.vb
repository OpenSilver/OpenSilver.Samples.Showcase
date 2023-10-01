Imports OpenSilver.Extensions.FileSystem
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
    Public Partial Class File_Save_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub ButtonSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Await FileSaver.SaveTextToFile("Hello World!", "MyTestFile.txt")
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "File_Save_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/File_Save_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "File_Save_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/File_Save_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FileSaver.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/FileSaver.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "File_Save_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/File_Save/File_Save_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FileSaver.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/File_Save/FileSaver.vb"
    }
})
        End Sub

    End Class
End Namespace
