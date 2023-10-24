using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class WebView_Demo : UserControl
    {
        public WebView_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonNavigate_Click(object sender, RoutedEventArgs e)
        {
            WebView1.Navigate(new Uri(TextBoxWithURL.Text));
        }
    }
}
