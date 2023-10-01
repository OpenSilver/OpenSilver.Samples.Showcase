Imports System
Imports System.Collections.Generic
#If SLMIGRATION Then
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

    Partial Public Class jQueryAjax_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Try
                Dim result = Await Extensions.MakeAjaxCall(url:="http://fiddle.jshell.net/echo/html/", data:="some sample text", type:="post")

                MessageBox.Show("The server returned the following result: " & result)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "jQueryAjax_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "jQueryAjax_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "jQueryAjaxHelper.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "jQueryAjax_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "jQueryAjaxHelper.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.vb"
    }
    })
        End Sub
    End Class
End Namespace