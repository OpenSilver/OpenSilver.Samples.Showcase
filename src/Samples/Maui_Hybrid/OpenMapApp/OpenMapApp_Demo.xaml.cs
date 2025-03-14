using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "location")]
    public partial class OpenMapApp_Demo : UserControl
    {
        public OpenMapApp_Demo()
        {
            InitializeComponent();
            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                SampleContainer.Children.Clear();
                SampleContainer.Children.Add(new TextBlock() { Text = "The map app is not supported in the browser.", TextWrapping = TextWrapping.Wrap });
            }
        }

        private void OpenMapAppButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var location = new Microsoft.Maui.Devices.Sensors.Location(48.8584, 2.2945);
                var options = new MapLaunchOptions { Name = "Eiffel tower" };

                try
                {
                    await Map.Default.OpenAsync(location, options);
                }
                catch (Exception ex)
                {
                    // No map application available to open
                }
            });
        }
    }
}
