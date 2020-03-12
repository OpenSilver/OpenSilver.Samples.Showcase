using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CSHTML5.Wrappers.DevExtreme.DataGrid;
using CSHTML5.Wrappers.DevExtreme.Common;
using DevExtreme_DataGrid.DevExpress.ui;

using DevExtreme_DataGrid.DevExpress.data;
using AnonymousTypes;

namespace CSHTML5.Wrappers.DevExtreme.DataGrid.Examples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            dxDataGrid.Configuration.LocationOfJquery = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/DevExtreme/scripts/jquery.min.js";
            dxDataGrid.Configuration.LocationOfDXAllJS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/DevExtreme/scripts/dx.all.js";
            dxDataGrid.Configuration.LocationOfDXCommonCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/DevExtreme/styles/dx.common.css";
            dxDataGrid.Configuration.LocationOfDXThemeCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/DevExtreme/styles/dx.light.css";
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            // Enter construction logic here...
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await DataGrid.JSInstanceLoaded; // Wait until the underlying JS instance of the DevExtreme DataGrid has been loaded

            SetColumnsData();

            SetDataSource();
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            DataGrid.UnsubscribeFromDataSourceEvent();
        }

        private void SetDataSource()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            Employee firstEmployee = new Employee(1, "John", "Heart", new DateTime(1995, 01, 15));
            Employee secondEmployee = new Employee(2, "Olivia", "Peyton", new DateTime(2012, 05, 14));
            Employee thirdEmployee = new Employee(3, "Robert", "Reagan", new DateTime(2002, 11, 18));

            employees.Add(firstEmployee);
            employees.Add(secondEmployee);
            employees.Add(thirdEmployee);

            DataGrid.DataSource = employees;
        }

        private void SetColumnsData()
        {
            List<dxDataGridColumn> columns = new List<dxDataGridColumn>();

            dxDataGridColumn idColumn = new dxDataGridColumn()
            {
                dataField = "ID",
                caption = "ID",
                dataType = "number",
                format = "",
                alignment = "left",
                allowGrouping = true
            };

			columns.Add(idColumn);

            dxDataGridColumn firstNameColumn = new dxDataGridColumn();
            firstNameColumn.dataField = "firstName";
            firstNameColumn.caption = "First Name";
            firstNameColumn.dataType = "string";
            firstNameColumn.format = "";
            firstNameColumn.alignment = "left";
            firstNameColumn.allowGrouping = true;
            columns.Add(firstNameColumn);

            dxDataGridColumn lastNameColumn = new dxDataGridColumn();
            lastNameColumn.dataField = "lastName";
            lastNameColumn.caption = "Last Name";
            lastNameColumn.dataType = "string";
            lastNameColumn.format = "";
            lastNameColumn.alignment = "left";
            lastNameColumn.allowGrouping = true;
            columns.Add(lastNameColumn);


            dxDataGridColumn hireDateColumn = new dxDataGridColumn();
            hireDateColumn.dataField = "hireDate";
            hireDateColumn.caption = "Hire Date";
            hireDateColumn.dataType = "date";
            hireDateColumn.format = "";
            hireDateColumn.alignment = "left";
            hireDateColumn.allowGrouping = true;
            columns.Add(hireDateColumn);


            DataGrid.Columns = columns;
        }

        void SearchPanel_Show(object sender, RoutedEventArgs e)
        {
            DataGrid.SearchPanel.visible = true;

            DataGrid.SetSearchPanelData(DataGrid.SearchPanel);
        }

        void SearchPanel_Hide(object sender, RoutedEventArgs e)
        {
            DataGrid.SearchPanel.visible = false;

            DataGrid.SetSearchPanelData(DataGrid.SearchPanel);
        }

        void Grouping_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.GroupPanelData.visible = true;

            DataGrid.SetGroupPanelData(DataGrid.GroupPanelData);
        }

        void Grouping_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.GroupPanelData.visible = false;

            DataGrid.SetGroupPanelData(DataGrid.GroupPanelData);
        }

        void ColumnsReordering_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.AllowColumnReordering = true;
        }

        void ColumnsReordering_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.AllowColumnReordering = false;
        }

        void ColorAlternation_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.RowAlternationEnabled = true;
        }

        void ColorAlternation_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.RowAlternationEnabled = false;
        }

        void ShowBorder_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.ShowBorders = true;
        }

        void ShowBorder_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.ShowBorders = false;
        }

        void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = new Employee(4, "Greta", "Sims", new DateTime(1998, 04, 23));

            DataGrid.AddObjectToDataSource(newEmployee);
        }

        void EnableTwoWayBinding_Click(object sender, RoutedEventArgs e)
        {
            DataGrid2.Visibility = Visibility.Visible;
            DataGrid2.ItemsSource = DataGrid.DataSource;
        }

        void Adding_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.DataGridEditing.allowAdding = true;
            DataGrid.DataGridEditing.allowUpdating = true;
            DataGrid.SetDataGridEditing(DataGrid.DataGridEditing);
        }

        void Adding_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.DataGridEditing.allowAdding = false;
            DataGrid.DataGridEditing.allowUpdating = false;
            DataGrid.SetDataGridEditing(DataGrid.DataGridEditing);
        }

        void Updating_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.DataGridEditing.allowUpdating = true;
            DataGrid.SetDataGridEditing(DataGrid.DataGridEditing);
        }

        void Updating_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.DataGridEditing.allowUpdating = false;
            DataGrid.SetDataGridEditing(DataGrid.DataGridEditing);
        }

        void Deleting_Enable(object sender, RoutedEventArgs e)
        {
            DataGrid.DataGridEditing.allowDeleting = true;
            DataGrid.SetDataGridEditing(DataGrid.DataGridEditing);
        }

        void Deleting_Disable(object sender, RoutedEventArgs e)
        {
            DataGrid.DataGridEditing.allowDeleting = false;
            DataGrid.SetDataGridEditing(DataGrid.DataGridEditing);
        }
    }
}
