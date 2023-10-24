Imports OpenSilver.Extensions.FileSystem
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class File_Save_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub ButtonSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Await FileSaver.SaveTextToFile("Hello World!", "MyTestFile.txt")
        End Sub
    End Class
End Namespace
