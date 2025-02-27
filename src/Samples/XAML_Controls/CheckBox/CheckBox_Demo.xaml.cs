using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "toggle", "boolean", "selection", "form")]
    public partial class CheckBox_Demo : UserControl
    {
        public CheckBox_Demo()
        {
            this.InitializeComponent();
        }

        void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You checked me.");
        }

        void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You unchecked me.");
        }
    }
}
