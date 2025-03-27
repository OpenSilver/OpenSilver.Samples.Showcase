using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "points")]
    public partial class ScatterSeries_Demo : ChartDemo
    {
        public ScatterSeries_Demo()
        {
            InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
