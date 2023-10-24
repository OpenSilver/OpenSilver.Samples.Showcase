using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class ListBox_Demo : UserControl
    {
        public ListBox_Demo()
        {
            this.InitializeComponent();

            ListBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
