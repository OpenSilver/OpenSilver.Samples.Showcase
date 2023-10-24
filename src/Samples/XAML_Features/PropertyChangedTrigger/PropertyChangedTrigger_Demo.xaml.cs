using System.Collections.Generic;

#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif
namespace OpenSilver.Samples.Showcase
{
    public partial class PropertyChangedTrigger_Demo : UserControl
    {
        public PropertyChangedTrigger_Demo()
        {
            this.InitializeComponent(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            borderText.Text = borderText.Text == "Yellow" ? "Orange" : "Yellow";
        }
    }
}