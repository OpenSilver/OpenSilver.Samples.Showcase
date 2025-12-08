using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("form", "input", "editor", "data", "binding", "datafield")]
    public partial class DataForm_Demo : UserControl
    {
        public DataForm_Demo()
        {
            InitializeComponent();

            // Populate the data form with the list of planets:
            DataForm1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
