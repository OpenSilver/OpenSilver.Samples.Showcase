using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "text", "entry", "form", "user input")]
    public partial class TextBox_Demo : UserControl
    {
        public TextBox_Demo()
        {
            this.InitializeComponent();
        }

        void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your name is: " + TextBoxName.Text);
        }
    }
}
