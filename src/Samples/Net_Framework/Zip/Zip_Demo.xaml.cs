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
    }
}
