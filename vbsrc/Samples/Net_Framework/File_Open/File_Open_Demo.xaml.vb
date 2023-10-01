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

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class File_Open_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub OnFileOpened(ByVal sender As Object, ByVal e As Extensions.FileOpenDialog.FileOpenedEventArgs)
            MessageBox.Show(e.Text)
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "File_Open_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/File_Open_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "File_Open_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/File_Open_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FileOpenDialog.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/FileOpenDialog.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "File_Open_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/File_Open/File_Open_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "FileOpenDialog.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/File_Open/FileOpenDialog.vb"
    }
})
        End Sub

    End Class
End Namespace
