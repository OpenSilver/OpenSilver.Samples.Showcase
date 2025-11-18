using OpenSilver.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("input", "selection", "list", "dropdown", "choices", "options", "comboboxitem")]
    public partial class ComboBox_Demo : UserControl
    {
        public ComboBox_Demo()
        {
            InitializeComponent();

            ComboBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
