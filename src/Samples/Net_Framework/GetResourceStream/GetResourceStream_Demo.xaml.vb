Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("resource", "stream", "file", "embedded resources", "load", "content")>
    Partial Public Class GetResourceStream_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ViewFile_Click(sender As Object, e As RoutedEventArgs)
            Dim uri As New Uri("/OpenSilver.Samples.Showcase;component/Other/SampleText.txt", UriKind.Relative)
            Dim content As String = RetrieveFileContent(uri)

            MessageBox.Show($"URI {uri} contains:{Environment.NewLine}" & content)
        End Sub

        Private Function RetrieveFileContent(uri As Uri) As String
            Dim resourceStream = Application.GetResourceStream(uri).Result
            Using currentReader As New StreamReader(resourceStream.Stream)
                Return currentReader.ReadToEnd()
            End Using
        End Function

    End Class

End Namespace
