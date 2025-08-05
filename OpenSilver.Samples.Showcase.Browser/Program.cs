using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using OpenSilver.Blazor;
using OpenSilver.Samples.Showcase;
using Syncfusion.Blazor;

namespace OpenSilver.Samples.Showcase.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Other.Internals.Licenses.SYNCFUSION_LICENSE);

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // For blazor components
            builder.UseBlazorForOpenSilver();
            builder.Services.AddRadzenSamples();
            builder.Services.AddMudBlazorSamples();
            builder.Services.AddBlazoriseSamples();
            builder.Services.AddDevExpressSamples();
            builder.Services.AddSyncfusionSamples();

            // For GeoBlazor:
            var inMemorySettings = new Dictionary<string, string?>
                                    {
                                        { "GeoBlazor:LicenseKey", Other.Internals.Licenses.GEOBLAZOR_LICENSE },
                                        { "ArcGISApiKey", Other.Internals.Licenses.ARCGIS_API_KEY }
                                    };
            builder.Configuration.AddInMemoryCollection(inMemorySettings);
            builder.Services.AddGeoBlazorSamples(builder.Configuration);



            var host = builder.Build();
            await host.RunAsync();
        }
    }
}