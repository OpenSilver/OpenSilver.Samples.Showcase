using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices;
using Radzen;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

public partial class Radzen_Sample : UserControl
{
    public Radzen_Sample()
    {
        InitializeComponent();

        var platform = DeviceInfo.Current.Platform;
        if (platform == DevicePlatform.iOS ||
            platform == DevicePlatform.MacCatalyst)
        {
            DropDownDataGrid.Visibility = Visibility.Collapsed;
            NumericDemo.Visibility = Visibility.Collapsed;
        }
        if (platform == DevicePlatform.iOS)
        {
            MaskDemo.Visibility = Visibility.Collapsed;
        }
    }
}

public static class Initializer
{
    public static void AddRadzenSamples(this IServiceCollection services)
    {
        services.AddRadzenComponents();
    }
}

