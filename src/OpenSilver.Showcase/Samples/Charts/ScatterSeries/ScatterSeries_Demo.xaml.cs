using OpenSilver.Showcase.Other;
using OpenSilver.Showcase.Search;

namespace OpenSilver.Showcase
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
