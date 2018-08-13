using kendo_ui.kendo.data;
using kendo_ui.kendo.ui;
using Samples.Third_Party.Telerik_KendoUI.Grid.Model;
using System.Collections.Generic;
using TypeScriptDefinitionsSupport;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Telerik_KendoUI.Grid
{
    public partial class Grid_Demo : Page
    {
        public Grid_Demo()
        {
            this.InitializeComponent();

            this.Loaded += Grid_Demo_Loaded;
        }

        private async void Grid_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Grid's underlying JS object has been loaded:
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            await Grid.JSObjLoaded;
            LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;

            Grid.JSObj.setOptions(new GridOptions()
            {
                columns = new JSArray<GridColumn>()
                {
                    new GridColumn()
                    {
                        field = "ContactName",
                        title = "Contact Name",
                        width = 150
                    },
                    new GridColumn()
                    {
                        field = "ContactTitle",
                        title = "Contact Title",
                    },
                    new GridColumn()
                    {
                        field = "CompanyName",
                        title = "Company Name"
                    },
                    new GridColumn()
                    {
                        field = "Country",
                        width = 150
                    }
                },
                pageable = new GridPageable()
                {
                    refresh = true,
                    pageSizes = true,
                    buttonCount = 5
                },
                dataSource = new DataSource(new DataSourceOptions()
                {
                    data = new JSArray<Contact>()
                    {
                        new Contact()
                        {
                            ContactName = "John Doe",
                            ContactTitle = "Developer",
                            CompanyName = "Userware",
                            Country = "FR"
                        },
                        new Contact()
                        {
                            ContactName = "Jane Wize",
                            ContactTitle = "Accountant",
                            CompanyName = "Mobicom",
                            Country = "US"
                        }
                    },
                    //type = "odata",
                    //transport = new DataSourceTransport()
                    //{
                    //    read = new DataSourceTransportRead()
                    //    {
                    //        url = "http://demos.telerik.com/kendo-ui/service/Northwind.svc/Customers"
                    //    }
                    //},
                    pageSize = 20
                })
            });
        }

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
