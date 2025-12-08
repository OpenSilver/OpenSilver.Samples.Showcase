using dymaptic.GeoBlazor.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class GeoBlazor_Sample : UserControl
    {
        public GeoBlazor_Sample()
        {
            this.InitializeComponent();
        }
    }

    public static class Initializer
    {
        public static void AddGeoBlazorSamples(this IServiceCollection services, IConfiguration? configuration)
        {
            services.AddGeoBlazor(configuration);
        }
    }
}
