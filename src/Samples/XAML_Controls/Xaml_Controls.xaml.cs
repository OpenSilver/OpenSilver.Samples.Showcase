using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

public partial class Xaml_Controls : Page
{
    public Xaml_Controls()
    {
        InitializeComponent();

        var dataGridDemoIndex = SamplesPanel.Children.IndexOf(DataGridDemo);
        SamplesPanel.Children.Insert(dataGridDemoIndex, new DataGridGrouping());
    }
}
