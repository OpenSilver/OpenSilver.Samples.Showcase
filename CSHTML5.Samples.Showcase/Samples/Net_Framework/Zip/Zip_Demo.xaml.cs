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
    }
}
