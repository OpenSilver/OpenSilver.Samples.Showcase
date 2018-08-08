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

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Syncfusion_EssentialJS1.Spreadsheet
{
    public partial class Spreadsheet_Demo : Page
    {
        public Spreadsheet_Demo()
        {
            this.InitializeComponent();

            this.Loaded += Spreadsheet_Demo_Loaded;
        }

        private async void Spreadsheet_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Spreadsheet's underlying JS object has been loaded:
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            await Spreadsheet.ExcelObjLoaded;
            LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
        }

            private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Spreadsheet_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Syncfusion_EssentialJS1/Spreadsheet/Spreadsheet_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Spreadsheet_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Syncfusion_EssentialJS1/Spreadsheet/Spreadsheet_Demo.xaml.cs"
                }
            });
        }
    }
}
