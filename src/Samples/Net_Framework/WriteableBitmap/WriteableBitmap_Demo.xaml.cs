using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("bitmap", "image", "graphics", "drawing", "rendering")]
    public partial class WriteableBitmap_Demo : UserControl
    {
        public WriteableBitmap_Demo()
        {
            InitializeComponent();

            if (Interop.IsRunningInTheSimulator) // simulator or MAUI Hybrid
            {
                LayoutRoot.Children.Clear();
                LayoutRoot.RowDefinitions.Clear();
                LayoutRoot.Children.Add(new TextBlock
                {
                    Text = "This feature is not supported on the current platform.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 12,
                });
            }
        }

        private void ClearButton_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            ivDestination.Source = null;
        }

        private async void MirrorButton_Click(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            var bitmap = new WriteableBitmap(200, 200);
            await bitmap.RenderAsync(ivSource, null/* TODO Change to default(_) if this is not a reference type */);

            // Let's modify pixels
            Array.Reverse(bitmap.Pixels);

            bitmap.Invalidate(); // Invalidate once Pixels are manipulated

            ivDestination.Source = bitmap;
        }
    }
}
