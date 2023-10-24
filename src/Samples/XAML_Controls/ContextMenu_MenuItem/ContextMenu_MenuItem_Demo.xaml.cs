using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class ContextMenu_MenuItem_Demo : UserControl
    {
        public ContextMenu_MenuItem_Demo()
        {
            this.InitializeComponent();
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu Item 1");
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu Item 2");
        }

        private void MenuItem3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu Item 3");
        }
    }
}
