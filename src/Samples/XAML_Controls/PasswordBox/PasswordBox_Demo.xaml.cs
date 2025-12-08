using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "password", "text", "entry", "form")]
    public partial class PasswordBox_Demo : UserControl
    {
        public PasswordBox_Demo()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayPasswordIfNotEmpty();
        }

        private void DisplayPasswordIfNotEmpty()
        {
            if (PasswordBox.Password.Length != 0)
                MessageBox.Show("The password typed is \n\"" + PasswordBox.Password + "\"");
            else
                MessageBox.Show("Please enter a password");
        }
    }
}
