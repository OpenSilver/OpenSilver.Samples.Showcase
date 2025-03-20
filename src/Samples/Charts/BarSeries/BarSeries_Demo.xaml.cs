using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("chart", "data", "visualization", "graph", "plot", "bar")]
    public partial class BarSeries_Demo : UserControl
    {
        public BarSeries_Demo()
        {
            this.InitializeComponent();


            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;
        }
    }
}
