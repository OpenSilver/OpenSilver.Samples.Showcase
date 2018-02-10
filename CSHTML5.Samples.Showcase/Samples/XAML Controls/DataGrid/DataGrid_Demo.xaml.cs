using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class DataGrid_Demo : UserControl
    {
        public DataGrid_Demo()
        {
            this.InitializeComponent();

            // Populate the data grids with the list of planets
            //DataGrid1.ItemsSource = Planet.GetListOfPlanets();
            //DataGrid2.ItemsSource = Planet.GetListOfPlanets();

            

        }
    }
}
