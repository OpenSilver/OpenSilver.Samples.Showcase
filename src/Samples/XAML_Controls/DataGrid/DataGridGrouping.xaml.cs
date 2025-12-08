using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;
using System.Windows.Data;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("data", "display", "grid", "table", "binding", "grouping")]
    public partial class DataGridGrouping : UserControl
    {
        public DataGridGrouping()
        {
            InitializeComponent();

            var pcv = new PagedCollectionView(Contact.People);
            pcv.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Contact.State)));
            pcv.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Contact.City)));
            dataGrid.ItemsSource = pcv;
        }
    }
}
