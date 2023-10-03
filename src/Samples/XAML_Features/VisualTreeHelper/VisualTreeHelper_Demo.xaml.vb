Imports System
Imports System.Collections.Generic
Imports System.Text
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
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
    Public Partial Class VisualTreeHelper_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub RevealTree_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim finalVisualTree = GetVisualTree(Me)
            MessageBox.Show(finalVisualTree)
        End Sub

        Public Shared Function GetVisualTree(ByVal parent As DependencyObject) As String
            Dim visualTreeStringBuilder As StringBuilder = New StringBuilder("Visual Tree : " & Environment.NewLine & Environment.NewLine)
            GetChildren(parent, visualTreeStringBuilder)
            Return visualTreeStringBuilder.ToString()
        End Function

        Private Shared Sub GetChildren(ByVal parent As DependencyObject, ByVal visualTreeStringBuilder As StringBuilder, ByVal Optional indentation As Integer = 0)
            Dim childrenCount = VisualTreeHelper.GetChildrenCount(parent)

            If childrenCount > 0 Then
                For i = 0 To childrenCount - 1
                    For e = 0 To indentation - 1
                        visualTreeStringBuilder.Append("    ")
                    Next

                    Dim currentChild = VisualTreeHelper.GetChild(parent, i)
                    visualTreeStringBuilder.AppendLine(currentChild.ToString())
                    GetChildren(currentChild, visualTreeStringBuilder, indentation + 1)
                Next
            End If
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "VisualTreeHelper_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "VisualTreeHelper_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "VisualTreeHelper_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
