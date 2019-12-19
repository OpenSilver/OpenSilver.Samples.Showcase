using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            // Populate the data grid with the list of planets:
            DataGrid1.ItemsSource = Planet.GetListOfPlanets();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DataGrid_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/DataGrid/DataGrid_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DataGrid_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/DataGrid/DataGrid_Demo.xaml.cs"
                }
            });
        }

        private void ButtonDisplayContextMenu_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.ContextMenu.IsOpen = true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            object content = b.Content;
            object tag = b.Tag;
        }
    }
}
