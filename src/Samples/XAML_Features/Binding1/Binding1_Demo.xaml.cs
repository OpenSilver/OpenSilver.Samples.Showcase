using OpenSilver.Samples.Showcase.Search;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("data", "MVVM", "binding", "UI")]
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
