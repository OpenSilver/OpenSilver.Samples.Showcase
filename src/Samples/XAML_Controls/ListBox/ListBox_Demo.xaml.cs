using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "selection", "list", "items", "choices")]
    public partial class ListBox_Demo : UserControl
    {
        public ListBox_Demo()
        {
            this.InitializeComponent();

            ListBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
