using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class SampleMaterialChildWindow : ChildWindow
    {
        public SampleMaterialChildWindow()
        {
            InitializeComponent();

            ObservableCollection<string> items = new ObservableCollection<string>() { "Item 1", "Item 2", "Item 3" };
            this.DataContext = items;
        }

        void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

