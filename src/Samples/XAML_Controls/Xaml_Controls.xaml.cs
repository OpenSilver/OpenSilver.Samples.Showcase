using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

public partial class Xaml_Controls : Page
{
    public Xaml_Controls()
    {
        InitializeComponent();

        var dataGridDemoIndex = SamplesPanel.Items.IndexOf(DataGridDemo);
        SamplesPanel.Items.Insert(dataGridDemoIndex, new DataGridGrouping { HorizontalAlignment = HorizontalAlignment.Center });
    }
}
