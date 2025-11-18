using OpenSilver.Showcase.Other;
using OpenSilver.Showcase.Search;

namespace OpenSilver.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "area", "points")]
    public partial class AreaSeries_Demo : ChartDemo
    {
        public AreaSeries_Demo()
        {
            InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
