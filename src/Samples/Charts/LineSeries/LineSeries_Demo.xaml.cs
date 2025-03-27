using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "line", "points")]
    public partial class LineSeries_Demo : ChartDemo
    {
        public LineSeries_Demo()
        {
            InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
