using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSilver.Blazor;
using OpenSilver.Samples.Showcase;
using Syncfusion.Blazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Other.Internals.Licenses.SYNCFUSION_LICENSE);

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //For lazy loading
            builder.Services.AddScoped<LazyAssemblyLoader>();
            builder.Services.AddScoped<ILazyFeatureNavigator, LazyFeatureNavigator>();

            // For blazor components
            builder.UseBlazorForOpenSilver();
            builder.Services.AddRadzenSamples();
            builder.Services.AddMudBlazorSamples();
            builder.Services.AddBlazoriseSamples();
            builder.Services.AddDevExpressSamples();
            builder.Services.AddSyncfusionSamples();

            //// For GeoBlazor:
            //var inMemorySettings = new Dictionary<string, string?>
            //                        {
            //                            { "GeoBlazor:LicenseKey", Other.Internals.Licenses.GEOBLAZOR_LICENSE },
            //                            { "ArcGISApiKey", Other.Internals.Licenses.ARCGIS_API_KEY }
            //                        };
            //builder.Configuration.AddInMemoryCollection(inMemorySettings);
            //builder.Services.AddGeoBlazorSamples(builder.Configuration);



            var host = builder.Build();

            //Make lazyLoader accessible:
            ServiceLocator.Provider = host.Services;

            await host.RunAsync();
        }
    }
}
