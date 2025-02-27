using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "keypress", "event", "interaction")]
    public partial class Keyboard_Demo : UserControl
    {
        public Keyboard_Demo()
        {
            this.InitializeComponent();
        }

        private void TextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                MessageBox.Show("You pressed Enter!" + Environment.NewLine + Environment.NewLine + "This is the text that you entered: " + TextBoxInput.Text);
        }
    }
}
