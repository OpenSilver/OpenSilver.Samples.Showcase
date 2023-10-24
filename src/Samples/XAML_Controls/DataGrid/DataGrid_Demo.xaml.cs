using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class DataGrid_Demo : UserControl
    {
        public DataGrid_Demo()
        {
            this.InitializeComponent();

            // Populate the data grid with the list of planets:
            DataGrid1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
