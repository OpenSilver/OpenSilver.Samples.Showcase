using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class RESX_Demo : UserControl
    {
        public RESX_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonReadResource_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show(SampleResourceFile.InfoMessage);
        }
    }
}
