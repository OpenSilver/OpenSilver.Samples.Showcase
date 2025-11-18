using OpenSilver.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Showcase
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
