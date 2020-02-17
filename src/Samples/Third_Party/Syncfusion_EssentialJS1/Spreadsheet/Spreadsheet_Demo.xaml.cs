using System.Collections.Generic;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Syncfusion_EssentialJS1.Spreadsheet
{
    public partial class Spreadsheet_Demo : Page
    {
        public Spreadsheet_Demo()
        {
            InitializeComponent();

            //Note: Below is an example of setting the location for the required scripts and css for the Syncfusion EssentialJS1 Spreadsheet control. See the tutorial at http://cshtml5.com for more information.
            ej_spreadsheet.ej.Spreadsheet.Configuration.LocationOfEjSpreadsheetJS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Syncfusion_EssentialJS1/scripts/ej.spreadsheet.min.js";
            ej_spreadsheet.ej.Spreadsheet.Configuration.LocationOfEjWebAllCss = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Syncfusion_EssentialJS1/themes/default-theme/ej.web.all.min.css";

            Loaded += Spreadsheet_Demo_Loaded;
        }

        private async void Spreadsheet_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Spreadsheet's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            bool result = await Spreadsheet.JSInstanceLoaded;
            if (result)
            {
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Spreadsheet_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Third_Party/Syncfusion_EssentialJS1/Spreadsheet/Spreadsheet_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Spreadsheet_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Third_Party/Syncfusion_EssentialJS1/Spreadsheet/Spreadsheet_Demo.xaml.cs"
                }
            });
        }
    }
}
