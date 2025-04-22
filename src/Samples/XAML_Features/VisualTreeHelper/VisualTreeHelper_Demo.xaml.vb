Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("logicaltreehelper", "UI elements", "XAML", "hierarchy", "UI", "treeview")>
    Partial Public Class VisualTreeHelper_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub RevealTree_Click(sender As Object, e As RoutedEventArgs)
            Dim viewer As New TreeViewerWindow(Me)
            viewer.Show()
        End Sub

    End Class

End Namespace
