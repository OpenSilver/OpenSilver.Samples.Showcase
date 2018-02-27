using CSHTML5.Extensions.FileSystem;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class Zip_Demo : UserControl
    {
        public Zip_Demo()
        {
            this.InitializeComponent();
        }

        async void OnFileOpened(object sender, CSHTML5.Extensions.FileOpenDialog.FileOpenedEventArgs e)
        {
            /*
            var javaScriptBlob = e.JavaScriptBlob;
            ZipFile zipFile = await ZipFile.Read(javaScriptBlob);
            ZipEntry entry = zipFile["MyTestFileInsideTheZIP.txt"];
            string content = entry.ExtractToString();
            MessageBox.Show(content);*/

            using (ZipFile zipFile = new ZipFile())
            {
                await zipFile.AddFile("file_compressed.txt", e.Text);
                var jsBlob = await zipFile.SaveToJavaScriptBlob();
                await FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip");
            }
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Zip_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Zip/Zip_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Zip_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Zip/Zip_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ZipFile.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Zip/ZipFile.cs"
                }
            });
        }
    }
}
