using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "selection", "list", "dropdown", "choices")]
    public partial class ComboBox_Demo : UserControl
    {
        public ComboBox_Demo()
        {
            this.InitializeComponent();

            ComboBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
