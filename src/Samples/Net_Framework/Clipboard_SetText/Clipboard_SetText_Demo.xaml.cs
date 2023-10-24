using System.Windows;
using System.Windows.Controls;

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
