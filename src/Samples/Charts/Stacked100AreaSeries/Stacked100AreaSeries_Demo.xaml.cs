using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "stacked", "points")]
    public partial class Stacked100AreaSeries_Demo : UserControl
    {
        public Stacked100AreaSeries_Demo()
        {
            this.InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
