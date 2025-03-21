using Microsoft.Maui.Devices;
using OpenSilver.Extensions.FileSystem;
using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("file", "save")]
    public partial class File_Save_Demo : UserControl
    {
        public File_Save_Demo()
        {
            InitializeComponent();

            var platform = DeviceInfo.Current.Platform;
            if (platform != DevicePlatform.Unknown && platform != DevicePlatform.WinUI)
            {
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(new TextBlock
                {
                    Text = "This feature is not supported on the current platform.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 12,
                });
            }
        }

        async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await FileSaver.SaveTextToFile("Hello World!", "MyTestFile.txt");
        }
    }
}
