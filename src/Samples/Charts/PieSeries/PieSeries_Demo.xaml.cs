using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "percentage")]
    public partial class PieSeries_Demo : UserControl
    {
        public PieSeries_Demo()
        {
            this.InitializeComponent();

            CostsSeries.ItemsSource = Sales.ProductionCosts;
        }
    }
}
