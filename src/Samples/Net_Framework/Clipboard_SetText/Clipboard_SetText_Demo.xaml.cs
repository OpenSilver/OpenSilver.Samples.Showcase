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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Clipboard_SetText_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Clipboard_SetText/Clipboard_SetText_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Clipboard_SetText_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Clipboard_SetText/Clipboard_SetText_Demo.xaml.cs"
                }
            });
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            Clipboard.SetText(ClipboardTextBox.Text);
            MessageBox.Show("Text copied!");
		}
	}
}
