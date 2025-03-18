using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("print", "document", "preview")]
    public partial class Printing_Demo : UserControl
    {
        public Printing_Demo()
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

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            var invoiceToPrint = new Invoice();
            CSHTML5.Native.Html.Printing.PrintManager.Print(invoiceToPrint);
        }
    }
}
