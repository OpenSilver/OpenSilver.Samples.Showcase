using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class ComboBox_Demo : UserControl
    {
        public ComboBox_Demo()
        {
            this.InitializeComponent();

            ComboBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
