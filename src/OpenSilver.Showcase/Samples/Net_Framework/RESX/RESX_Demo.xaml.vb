Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase

    <SearchKeywords("resources", "localization", "RESX", "translation")>
    Partial Public Class RESX_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ButtonReadResource_Click(sender As Object, e As RoutedEventArgs)
            MessageBox.Show($"SampleResourceFile.InfoMessage: {SampleResourceFile.InfoMessage}")
        End Sub

        Private Sub Hyperlink_Click(sender As Object, e As RoutedEventArgs)
            MainPage.Current.PageContainer.Navigate(New Uri("/XAML_Features/MarkupExtensions", UriKind.Relative))
        End Sub

    End Class

End Namespace
