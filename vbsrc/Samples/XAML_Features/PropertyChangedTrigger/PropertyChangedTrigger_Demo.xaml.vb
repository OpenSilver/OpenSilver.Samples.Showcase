Imports System.Collections.Generic

#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#End If
Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class PropertyChangedTrigger_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "PropertyChangedTrigger_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "PropertyChangedTrigger_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "PropertyChangedTrigger_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.vb"
    }
})
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.borderText.Text = If(Equals(Me.borderText.Text, "Yellow"), "Orange", "Yellow")
        End Sub
    End Class
End Namespace
