using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices;
using Radzen;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public partial class Radzen_Sample : UserControl
    {
        public Radzen_Sample()
        {
            InitializeComponent();

            if (DeviceInfo.Current.Platform == DevicePlatform.iOS ||
                DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
            {
                DropDownDataGrid.Visibility = Visibility.Collapsed;
                NumericDemo.Visibility = Visibility.Collapsed;
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
}

