using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
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
