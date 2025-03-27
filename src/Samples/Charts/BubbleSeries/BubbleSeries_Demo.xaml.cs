using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "bubble", "points")]
    public partial class BubbleSeries_Demo : ChartDemo
    {
        public BubbleSeries_Demo()
        {
            InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
