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
    Public Partial Class GetDiv_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonSetCSS_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim div = Interop.GetDiv(Me)

            Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div)

            ' Note: refer to the documentation at: http://opensilver.net/links/how-to-call-javascript.aspx
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "GetDiv_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "GetDiv_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "GetDiv_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
