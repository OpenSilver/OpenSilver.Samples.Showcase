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
