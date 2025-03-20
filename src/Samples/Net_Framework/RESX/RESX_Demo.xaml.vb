Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("resources", "localization", "RESX", "translation")>
    Partial Public Class RESX_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonReadResource_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            MessageBox.Show(SampleResourceFileVB.InfoMessage)
        End Sub
    End Class
End Namespace
