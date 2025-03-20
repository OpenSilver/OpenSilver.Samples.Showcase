Imports OpenSilver.Samples.Showcase.Search
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("visual tree", "UI elements", "XAML", "hierarchy", "UI")>
    Partial Public Class VisualTreeHelper_Demo
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
    End Class
End Namespace
