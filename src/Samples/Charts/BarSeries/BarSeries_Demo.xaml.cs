using OpenSilver.Samples.Showcase.Other;
using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

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
