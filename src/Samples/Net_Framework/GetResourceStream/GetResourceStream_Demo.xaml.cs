using OpenSilver.Samples.Showcase.Search;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("resource", "stream", "file", "embedded resources", "load", "content")]
public partial class GetResourceStream_Demo : UserControl
{
    public GetResourceStream_Demo()
    {
        InitializeComponent();
    }

    private void ViewFile_Click(object sender, RoutedEventArgs e)
    {
        var uri = new Uri("/OpenSilver.Samples.Showcase;component/Other/SampleText.txt", UriKind.Relative);
        var content = RetrieveFileContent(uri);

        MessageBox.Show($"URI {uri} contains:\n" + content);
    }

    private string RetrieveFileContent(Uri uri)
    {
        var resourceStream = Application.GetResourceStream(uri).Result;
        using var currentReader = new StreamReader(resourceStream.Stream);

        string result = currentReader.ReadToEnd();
        return result;
    }
}