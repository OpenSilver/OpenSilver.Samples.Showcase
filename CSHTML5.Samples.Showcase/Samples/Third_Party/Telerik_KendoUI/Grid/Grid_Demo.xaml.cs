using CSHTML5.Extensions.Common;
using kendo_ui_grid.kendo.data;
using kendo_ui_grid.kendo.ui;
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
            InitializeComponent();

            Loaded += Grid_Demo_Loaded;
        }

        private async void Grid_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Grid's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            await Grid.JSInstanceLoaded;
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

            //Grid.ItemsSource = new List<Contact>()
            //{
            //    new Contact()
            //    {
            //        ContactName = "John Doe",
            //        ContactTitle = "Developer",
            //        CompanyName = "Userware",
            //        Country = "FR"
            //    },
            //    new Contact()
            //    {
            //        ContactName = "Jane Wize",
            //        ContactTitle = "Accountant",
            //        CompanyName = "Mobicom",
            //        Country = "US"
            //    }
            //};

            //var options = Grid.getOptions();
            //options.columns = new JSArray<GridColumn>()
            //{
            //    new GridColumn()
            //    {
            //        field = "ContactName",
            //        title = "Contact Name",
            //        width = 150
            //    },
            //    new GridColumn()
            //    {
            //        field = "ContactTitle",
            //        title = "Contact Title",
            //    },
            //    new GridColumn()
            //    {
            //        field = "CompanyName",
            //        title = "Company Name"
            //    },
            //    new GridColumn()
            //    {
            //        field = "Country",
            //        width = 150
            //    }
            //};
            //Grid.setOptions(options);
            //Grid.setDataSource(new DataSource(new DataSourceOptions()
            //{
            //    //data = new JSArray<Contact>()
            //    //{
            //    //    new Contact()
            //    //    {
            //    //        ContactName = "John Doe",
            //    //        ContactTitle = "Developer",
            //    //        CompanyName = "Userware",
            //    //        Country = "FR"
            //    //    },
            //    //    new Contact()
            //    //    {
            //    //        ContactName = "Jane Wize",
            //    //        ContactTitle = "Accountant",
            //    //        CompanyName = "Mobicom",
            //    //        Country = "US"
            //    //    }
            //    //}
            //    type = "odata",
            //    transport = new DataSourceTransport()
            //    {
            //        read = new DataSourceTransportRead()
            //        {
            //            url = new JSObject("http://demos.telerik.com/kendo-ui/service/Northwind.svc/Customers")
            //        }
            //    }
            //}));
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
