using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("data", "display", "grid", "table", "binding", "datapager")]
    public partial class DataGrid_Demo : UserControl
    {
        public DataGrid_Demo()
        {
            InitializeComponent();

            // Populate the data grid with the list of planets:
            DataGrid1.ItemsSource = AtomicElements.Elements;
        }
    }
}
