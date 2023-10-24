using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Binding1_Demo : UserControl
    {
        public Binding1_Demo()
        {
            this.InitializeComponent();

            ObservableCollection<Planet> listOfPlanets = Planet.GetListOfPlanets();
            ItemsControl1.ItemsSource = listOfPlanets;
            ContentControl1.Content = listOfPlanets[0];
        }

        private void ButtonPlanet_Click(object sender, RoutedEventArgs e)
        {
            var planet = ((Button)sender).DataContext;
            ContentControl1.Content = planet;
        }
    }
}
