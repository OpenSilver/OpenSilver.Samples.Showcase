using OpenSilver.Samples.Showcase.Search;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("compression", "file", "archive", "savefiledialog")]
public partial class Zip_Demo : UserControl
{
    public Zip_Demo()
    {
        InitializeComponent();
    }

    private async void ButtonGenerateZip_Click(object sender, RoutedEventArgs e)
    {
        using var memoryStream = new MemoryStream();

        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
        {
            var zipEntry = archive.CreateEntry("SampleText.txt", CompressionLevel.Optimal);
            using var entryStream = zipEntry.Open();
            using var writer = new StreamWriter(entryStream);
            writer.Write("Hello World!");
        }

        var dialog = new Controls.SaveFileDialog
        {
            DefaultExt = ".zip",
            Filter = "Zip files (*.zip)|*.zip|All files (*.*)|*.*",
            DefaultFileName = "MyTestFile"
        };

        if (await dialog.ShowDialogAsync() == true)
        {
            var data = memoryStream.ToArray();
            using var saveFileStream = await dialog.OpenFileAsync();
            await saveFileStream.WriteAsync(data, 0, data.Length);
            await saveFileStream.FlushAsync();
        }
    }
}
