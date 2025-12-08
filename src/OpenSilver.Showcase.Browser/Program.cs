using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
#if WITHBLAZOR
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.Extensions.Configuration;
using OpenSilver.Blazor;
using OpenSilver.Showcase;
using System.Collections.Generic;
#if FULLBLAZOR
using Syncfusion.Blazor;
#endif
#endif

namespace OpenSilver.Showcase.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
#if FULLBLAZOR
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Other.Internals.Licenses.SYNCFUSION_LICENSE); 
#endif

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

#if WITHBLAZOR
            //For lazy loading
            builder.Services.AddScoped<LazyAssemblyLoader>();
            builder.Services.AddScoped<ILazyFeatureNavigator, LazyFeatureNavigator>();

            // For blazor components
            builder.UseBlazorForOpenSilver();
            builder.Services.AddRadzenSamples();
            builder.Services.AddMudBlazorSamples();
            builder.Services.AddBlazoriseSamples();
#if FULLBLAZOR
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
#endif
#endif



            var host = builder.Build();

#if WITHBLAZOR
            //Make lazyLoader accessible:
            ServiceLocator.Provider = host.Services; 
#endif

            await host.RunAsync();
        }
    }
}
