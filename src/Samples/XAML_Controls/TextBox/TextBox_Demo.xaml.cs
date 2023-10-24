using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
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
