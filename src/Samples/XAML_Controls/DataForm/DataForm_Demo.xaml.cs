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
    [SearchKeywords("form", "input", "editor", "data", "binding")]
    public partial class DataForm_Demo : UserControl
    {
        public DataForm_Demo()
        {
            this.InitializeComponent();

            // Populate the data form with the list of planets:
            DataForm1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
