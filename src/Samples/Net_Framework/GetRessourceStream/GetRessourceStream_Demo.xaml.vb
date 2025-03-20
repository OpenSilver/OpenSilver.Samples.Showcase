Imports OpenSilver.Samples.Showcase.Search
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("resource", "stream", "file access", "embedded resources", "load")>
    Partial Public Class GetRessourceStream_Demo
        Inherits UserControl
        Private currentUri As Uri


        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Async Sub ViewFile_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            currentUri = New Uri("ms-appx:/Other/SampleText.txt")
            Dim GetFileContent = RetrieveFileContent(currentUri)

            Dim currentFileContent = Await GetFileContent
            MessageBox.Show("Contains: " & currentFileContent)

        End Sub

        Private Async Function RetrieveFileContent(ByVal uri As Uri) As Task(Of String)
            Dim resourceStream = Await Application.GetResourceStream(uri)
            Dim currentReader As StreamReader = New StreamReader(resourceStream.Stream)

            Using currentReader
                MessageBox.Show("Opening : " & uri.AbsoluteUri)
                Dim result As String = Await currentReader.ReadToEndAsync()
                Return result
            End Using

        End Function
    End Class
End Namespace
