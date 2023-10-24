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
