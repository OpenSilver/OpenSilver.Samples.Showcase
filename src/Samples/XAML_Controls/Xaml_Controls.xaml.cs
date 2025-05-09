using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace OpenSilver.Samples.Showcase;

public partial class Xaml_Controls : Page
{
    public Xaml_Controls()
    {
        InitializeComponent();

        var dataGridDemoIndex = SamplesPanel.Items.IndexOf(DataGridDemo);
        SamplesPanel.Items.Insert(dataGridDemoIndex, new DataGridGrouping());
    }
}
