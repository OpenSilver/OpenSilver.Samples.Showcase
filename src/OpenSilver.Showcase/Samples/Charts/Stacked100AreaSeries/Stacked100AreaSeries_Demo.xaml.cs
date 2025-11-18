using OpenSilver.Showcase.Other;
using OpenSilver.Showcase.Search;

namespace OpenSilver.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "stacked", "points")]
    public partial class Stacked100AreaSeries_Demo : ChartDemo
    {
        public Stacked100AreaSeries_Demo()
        {
            InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
