using OpenSilver.Samples.Showcase.Search;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("bitmap", "image", "graphics", "drawing", "rendering")]
    public partial class WriteableBitmap_Demo : UserControl
    {
        public WriteableBitmap_Demo()
        {
            this.InitializeComponent();
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
