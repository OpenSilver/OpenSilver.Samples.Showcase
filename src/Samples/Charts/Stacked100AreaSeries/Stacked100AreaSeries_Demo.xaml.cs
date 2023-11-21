using OpenSilver.Samples.Showcase.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class Stacked100AreaSeries_Demo : UserControl
    {
        public Stacked100AreaSeries_Demo()
        {
            this.InitializeComponent();

            ChairsSeries.ItemsSource = Sales.Chairs;
            TablesSeries.ItemsSource = Sales.Tables;


            //var s1 = new SeriesDefinition();
            //MySeries.SeriesDefinitions.Add(s1);
            //s1.Title = "Chairs";
            //s1.ItemsSource = Sales.Chairs;
            //s1.IndependentValueBinding = new Binding("Month");
            //s1.DependentValueBinding = new Binding("Sales");

            //var s2 = new SeriesDefinition();
            //MySeries.SeriesDefinitions.Add(s2);
            //s2.Title = "Tables";
            //s2.ItemsSource = Sales.Tables;
            //s2.IndependentValueBinding = new Binding("Month");
            //s2.DependentValueBinding = new Binding("Sales");
        }
    }
}
