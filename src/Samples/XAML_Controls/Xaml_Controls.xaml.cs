using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

public partial class Xaml_Controls : UserControl
{
    public Xaml_Controls()
    {
        InitializeComponent();

        // todo:
        ThumbDemo.Visibility = Visibility.Collapsed;

        var dataGridDemoIndex = SamplesPanel.Children.IndexOf(DataGridDemo);
        SamplesPanel.Children.Insert(dataGridDemoIndex, new DataGridGrouping());
    }
}
