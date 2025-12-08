using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "percentage")]
    public partial class PieSeries_Demo : ChartDemo
    {
        public PieSeries_Demo()
        {
            InitializeComponent();

            CostsSeries.ItemsSource = Sales.ProductionCosts;
        }
    }
}
