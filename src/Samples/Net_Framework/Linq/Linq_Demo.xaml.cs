using OpenSilver.Samples.Showcase.Search;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("query", "queries", "data processing", "collections")]
    public partial class Linq_Demo : UserControl
    {
        public Linq_Demo()
        {
            this.InitializeComponent();
        }

        void ButtonToDemonstrateLinq_Click(object sender, RoutedEventArgs e)
        {
            var planets = Planet.GetListOfPlanets();

            var result = from p in planets
                         where p.Radius > 7000
                         orderby p.Name
                         select p.Name;

            MessageBox.Show(string.Format("List of planets that have a radius greater than 7000km sorted alphabetically: {0}", string.Join(", ", result)));
        }
    }
}
