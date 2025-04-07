using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Xaml_Controls : UserControl
    {
        public Xaml_Controls()
        {
            InitializeComponent();

#if OPENSILVER
            NonModalChildWindow.Visibility = Visibility.Collapsed;
#endif
            ScrollBarDemo.Visibility = Visibility.Collapsed;
            ThumbDemo.Visibility = Visibility.Collapsed;

            var dataGridDemoIndex = SamplesPanel.Children.IndexOf(DataGridDemo);
            SamplesPanel.Children.Insert(dataGridDemoIndex, new DataGridGrouping());
        }
    }
}
