using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "screen", "information", "pixel", "display")]
    public partial class DisplayInfo_Demo : UserControl
    {
        public DisplayInfo_Demo()
        {
            InitializeComponent();
            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                SampleContainer.Children.Clear();
                SampleContainer.Children.Add(new TextBlock() { Text = "The display info sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap });
            }
        }

        private void ButtonGetDisplayInfo_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var sb = new StringBuilder();

                    sb.AppendLine($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
                    sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");
                    sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}");
                    sb.AppendLine($"Rotation: {DeviceDisplay.Current.MainDisplayInfo.Rotation}");
                    sb.AppendLine($"Refresh Rate: {DeviceDisplay.Current.MainDisplayInfo.RefreshRate}");

                    DisplayInfo.Text = sb.ToString();
                }
                catch
                {
                }
            });
        }
    }
}
