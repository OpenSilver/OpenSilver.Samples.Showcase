using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "screen", "information", "pixel")]
    public partial class DisplayInfo_Demo : UserControl
    {
        public DisplayInfo_Demo()
        {
            this.InitializeComponent();

            if (!Vibration.Default.IsSupported)
            {
                SampleContainer.Children.Clear();
                SampleContainer.Children.Add(new TextBlock() { Text = "This device does not support getting the display information.", TextWrapping = TextWrapping.Wrap });
            }
        }

        private void ButtonGetDisplayInfo_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

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
