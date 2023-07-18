using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

namespace OpenSilver.Samples.Showcase
{
    public partial class Material_Design : UserControl
    {
        public Material_Design()
        {
            InitializeComponent();

            ObservableCollection<string> items = new ObservableCollection<string>() { "Item 1", "Item 2", "Item 3" };
            DataContext = items;

            DataGrid.ItemsSource = DataGridDataInstance.GetDataSet();
        }

        private void DisplayContextMenu_Click(object sender, MouseButtonEventArgs e)
        {
            ContentControl cc = (ContentControl)sender;
            cc.ContextMenu.HorizontalOffset = e.GetPosition(null).X;
            cc.ContextMenu.VerticalOffset = e.GetPosition(null).Y;
            cc.ContextMenu.IsOpen = true;
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = (ContextMenu)sender;
            contextMenu.HorizontalOffset = 0;
            contextMenu.VerticalOffset = 0;
        }

        private void ButtonShowChildWindow_Click(object sender, RoutedEventArgs e)
        {
            var childWindow = new SampleMaterialChildWindow();
            childWindow.Show();
        }

        private void MenuEditDraft_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string message = String.Format(@"You clicked to edit the draft on the item ""{0}"".", ((DataGridDataInstance)menuItem.DataContext).Name);
            MessageBox.Show(message);
        }

        private void MenuRemoveDraft_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string message = String.Format(@"You clicked to remove the draft on the item ""{0}"".", ((DataGridDataInstance)menuItem.DataContext).Name);
            MessageBox.Show(message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            object content = b.Content;
            object tag = b.Tag;
        }
    }


    public class DataGridDataInstance
    {
        DataGridDataInstance(string name, string location, string subscription)
        {
            Name = name;
            Location = location;
            Subscription = subscription;
        }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Subscription { get; set; }

        public static ObservableCollection<DataGridDataInstance> GetDataSet()
        {
            return new ObservableCollection<DataGridDataInstance>()
            {
                new DataGridDataInstance("Default-ApplicationInsights-EastUS", "East US", "Subscription-1"),
                new DataGridDataInstance("Default-SQL-CentralUS", "Central US", "Subscription-1"),
                new DataGridDataInstance("Default-SQL-NorthCentralUS", "North Central US", "Subscription-1"),
                new DataGridDataInstance("Default-SQL-SouthCentralUS", "South Central US", "Subscription-1"),
                new DataGridDataInstance("Default-Storage-NorthCentralUS", "North Central US", "Subscription-1"),
                new DataGridDataInstance("Default-Storage-WestUS", "West US", "Subscription-1"),
                new DataGridDataInstance("Default-Web-EastUS", "East US", "Subscription-1"),
                new DataGridDataInstance("Default-Web-NorthCentralUS", "North Central US", "Subscription-1"),
            };
        }
    }
}
