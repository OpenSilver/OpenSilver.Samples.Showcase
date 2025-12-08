using Microsoft.Maui.Devices;
using OpenSilver.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("WebBrowser", "browser", "embed", "web content", "UI")]
    public partial class WebView_Demo : UserControl
    {
        public WebView_Demo()
        {
            this.InitializeComponent();

            // If the app includes a browser that can open any URL, it will be rated 18+ in app stores.
            // Therefore, we disable the ability to enter arbitrary URLs.
            TextBoxWithURL.IsEnabled = DeviceInfo.Current.Platform == DevicePlatform.Unknown;
        }

        private void ButtonNavigate_Click(object sender, RoutedEventArgs e)
        {
            WebView1.Navigate(new Uri(TextBoxWithURL.Text));
        }
    }
}
