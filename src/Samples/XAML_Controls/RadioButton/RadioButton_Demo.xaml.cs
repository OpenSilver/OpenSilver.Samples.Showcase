using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "toggle", "option", "selection", "form")]
    public partial class RadioButton_Demo : UserControl
    {
        public RadioButton_Demo()
        {
            this.InitializeComponent();
        }

        void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(RadioButton1.IsChecked == true ? "Option 1 selected" : "Option 2 selected");
            });
        }
    }
}
