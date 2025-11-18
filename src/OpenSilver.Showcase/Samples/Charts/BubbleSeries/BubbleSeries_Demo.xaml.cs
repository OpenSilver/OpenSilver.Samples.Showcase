using OpenSilver.Showcase.Other;
using OpenSilver.Showcase.Search;

namespace OpenSilver.Showcase
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
