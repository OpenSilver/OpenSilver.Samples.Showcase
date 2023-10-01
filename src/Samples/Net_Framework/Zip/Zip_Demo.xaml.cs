using OpenSilver.Extensions.FileSystem;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
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

        //async void OnFileOpened(object sender, OpenSilver.Extensions.FileOpenDialog.FileOpenedEventArgs e)
        //{
        //    var javaScriptBlob = e.JavaScriptBlob;
        //    ZipFile zipFile = await ZipFile.Read(javaScriptBlob);
        //    ZipEntry entry = zipFile["MyTestFileInsideTheZIP.txt"];
        //    string content = entry.ExtractToString();
        //    MessageBox.Show(content);
        //}

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Zip_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Zip_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ZipFile.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/ZipFile.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Zip_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/Zip/Zip_Demo.xaml.vb"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ZipFile.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Net_Framework/Zip/ZipFile.vb"
                }
            });
        }
    }
}
