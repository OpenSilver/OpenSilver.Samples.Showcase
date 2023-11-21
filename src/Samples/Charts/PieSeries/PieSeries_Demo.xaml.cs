using OpenSilver.Samples.Showcase.Other;
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
    public partial class PieSeries_Demo : UserControl
    {
        public PieSeries_Demo()
        {
            this.InitializeComponent();

            CostsSeries.ItemsSource = Sales.ProductionCosts;
        }
    }
}
