using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class DataGrid_Demo : UserControl
    {
        public DataGrid_Demo()
        {
            this.InitializeComponent();

            // Populate the data grid with the list of planets:
            DataGrid1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
