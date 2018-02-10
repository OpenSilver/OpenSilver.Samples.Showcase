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
    public partial class ComboBox_Demo : UserControl
    {
        public ComboBox_Demo()
        {
            this.InitializeComponent();

            ComboBox1.ItemsSource = Planet.GetListOfPlanets();
        }
    }
}
