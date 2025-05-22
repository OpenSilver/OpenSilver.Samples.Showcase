Imports System.Windows
Imports System.Windows.Browser
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("HTML", "browser", "web", "host", "useragent", "platform")>
    Partial Public Class HtmlPage_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            documentUriTextBlock.Text = HtmlPage.Document.DocumentUri.OriginalString
        End Sub
    End Class

End Namespace
