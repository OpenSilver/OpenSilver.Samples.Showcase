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

namespace CSHTML5.Samples.Showcase
{
    public partial class ComboBox_Demo : UserControl
    {
        public ComboBox_Demo()
        {
            this.InitializeComponent();

            ComboBox1.ItemsSource = Planet.GetListOfPlanets();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ComboBox_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ComboBox_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/ComboBox_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DefaultComboBoxStyle.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ComboBox/Styles/DefaultComboBoxStyle.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Planet.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Other/Planet.cs"
                }
            });
        }

    }
}
