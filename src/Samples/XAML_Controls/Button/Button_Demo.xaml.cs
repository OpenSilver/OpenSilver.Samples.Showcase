using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("click", "interaction", "command")]
    public partial class Button_Demo : UserControl
    {
        public Button_Demo()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button!");
        }
    }
}
