using Ionic.Zip;
using Microsoft.Maui.Devices;
using OpenSilver.Extensions.FileSystem;
using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("compression", "jszip", "file", "archive", "interop", "ionic")]
public partial class JSZip_Demo : UserControl
{
    public JSZip_Demo()
    {
        InitializeComponent();

        var platform = DeviceInfo.Current.Platform;
        if (platform == DevicePlatform.Android || platform == DevicePlatform.iOS)
        {
            SavingNotSupportedLabel.Visibility = Visibility.Visible;
        }
    }

    private async void ButtonGenerateZip_Click(object sender, RoutedEventArgs e)
    {
        var zipFile = new ZipFile();
        await zipFile.AddFile("SampleText.txt", "Hello World!");
        var jsBlob = await zipFile.SaveToJavaScriptBlob();

        if (jsBlob != null)
        {
            await FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip");
        }
    }
}
