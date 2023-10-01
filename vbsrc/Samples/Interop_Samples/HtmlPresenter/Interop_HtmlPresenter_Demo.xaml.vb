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
    Public Partial Class Interop_HtmlPresenter_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonClickHere_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call MessageBox.Show("The value is: " & Me.NumericTextBox1.Value.ToString())
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Interop_HtmlPresenter_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/HtmlPresenter/Interop_HtmlPresenter_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Interop_HtmlPresenter_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/HtmlPresenter/Interop_HtmlPresenter_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "NumericTextBox.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/HtmlPresenter/NumericTextBox.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Interop_HtmlPresenter_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Interop_Samples/HtmlPresenter/Interop_HtmlPresenter_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "NumericTextBox.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Interop_Samples/HtmlPresenter/NumericTextBox.vb"
    }
})
        End Sub

    End Class
End Namespace
