using OpenSilver.Showcase.Other;
using OpenSilver.Showcase.Search;

namespace OpenSilver.Showcase
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
