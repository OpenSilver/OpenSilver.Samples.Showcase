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
        
        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Linq_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Linq/Linq_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Linq_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Linq/Linq_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Planet.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Other/Planet.cs"
                }
            });
        }

    }
}
