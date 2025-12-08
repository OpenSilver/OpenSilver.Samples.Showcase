using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "selection", "list", "items", "choices", "listboxitem", "template")]
    public partial class ListBox_Demo : UserControl
    {
        public ListBox_Demo()
        {
            InitializeComponent();

            ListBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
