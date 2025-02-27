using OpenSilver.Extensions.FileSystem;
using Ionic.Zip;
using System.Windows;
using System.Windows.Controls;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("compression", "zip", "file", "archive")]
    public partial class Zip_Demo : UserControl
    {
        public Zip_Demo()
        {
            this.InitializeComponent();
        }

        private async void ButtonGenerateZip_Click(object sender, RoutedEventArgs e)
        {
            using (ZipFile zipFile = new ZipFile())
            {
                await zipFile.AddFile("SampleText.txt", "Hello World!");
                var jsBlob = await zipFile.SaveToJavaScriptBlob();
                await FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip");
            }
        }
    }
}
