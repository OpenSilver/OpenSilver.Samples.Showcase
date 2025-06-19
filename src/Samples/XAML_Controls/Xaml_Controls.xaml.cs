using Microsoft.Maui.Devices;
using System;
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

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            DeferLoadingItemsBehavior.Attach(
                 SamplesPanel,
                 loadItemsHeight: Application.Current.Host.Content.ActualHeight * 3,
                 maxLoadingTime: TimeSpan.FromSeconds(4),
                 bottomScrollOffset: 20
             );
        }
    }
}
