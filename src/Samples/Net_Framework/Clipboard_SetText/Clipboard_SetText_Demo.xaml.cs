using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
#if SLMIGRATION
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
    public partial class Clipboard_SetText_Demo : UserControl
    {
        public Clipboard_SetText_Demo()
        {
            this.InitializeComponent();
        }

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
            await Clipboard.SetTextAsync(ClipboardTextBox.Text);
            MessageBox.Show("Text copied!");
		}
	}
}
