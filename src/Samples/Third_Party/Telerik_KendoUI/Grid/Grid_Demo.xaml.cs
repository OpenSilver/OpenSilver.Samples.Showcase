#if !OPENSILVER
using CSHTML5.Wrappers.KendoUI.Common;
using kendo_ui_grid.kendo.data;
using kendo_ui_grid.kendo.ui;
#endif
using System.Collections.Generic;
using TypeScriptDefinitionsSupport;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Telerik_KendoUI.Grid
{
    public partial class Grid_Demo : Page
    {
        public Grid_Demo()
        {

#if !OPENSILVER
            InitializeComponent();

            //Note: Below is an example of setting the location for the required scripts and css for the KendoUI Grid control. See the tutorial at http://cshtml5.com for more information.
            kendo_ui_grid.kendo.ui.Grid.Configuration.LocationOfKendoAllJS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/scripts/kendo.all.min.js";
            kendo_ui_grid.kendo.ui.Grid.Configuration.LocationOfKendoCommonMaterialCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.common-material.min.css";
            kendo_ui_grid.kendo.ui.Grid.Configuration.LocationOfKendoMaterialCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.material.min.css";
            kendo_ui_grid.kendo.ui.Grid.Configuration.LocationOfKendoMaterialMobileCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.material.mobile.min.css";
            kendo_ui_grid.kendo.ui.Grid.Configuration.LocationOfKendoRTLCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.rtl.min.css";



            Loaded += Grid_Demo_Loaded;
#endif
        }

#if !OPENSILVER
        private async void Grid_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Grid's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            bool result = await Grid.JSInstanceLoaded;
            if (!result)
            {
                Grid.Visibility = Visibility.Visible;
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
                Grid.Visibility = Visibility.Visible;

                Grid.setOptions(new GridOptions()
                {
                    pageable = new GridPageable()
                    {
                        refresh = true,
                        pageSizes = true,
                        buttonCount = 5
                    },
                    editable = true
                });
                Grid.dataSource.pageSize(4);
            }
    }
#endif

    private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Grid_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Telerik_KendoUI/Grid/Grid_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Grid_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Telerik_KendoUI/Grid/Grid_Demo.xaml.cs"
                }
            });
        }
    }
}
