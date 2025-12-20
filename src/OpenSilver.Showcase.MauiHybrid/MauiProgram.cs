#if WITHBLAZOR
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using DevExpress.Blazor;
using MudBlazor.Services;
using Radzen;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
#endif
//using dymaptic.GeoBlazor.Core;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenSilver.MauiHybrid.Runner;
using System.Globalization;
using Microsoft.AspNetCore.Components.Infrastructure;

namespace OpenSilver.Showcase.MauiHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var culture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        // For all new threads:
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

#if WITHBLAZOR
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Other.Internals.Licenses.SYNCFUSION_LICENSE); 
#endif

        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .ConfigureMauiHandlers(conf =>
            {
#if ANDROID
                conf.AddHandler<BlazorWebView, AndroidWebViewHandler>();
#endif
            });

#if WITHBLAZOR
        //For lazy loading
        builder.Services.AddScoped<LazyAssemblyLoader>();
        builder.Services.AddScoped<ILazyFeatureNavigator, LazyFeatureNavigator>(); 
#endif

        builder.Services.AddScoped<IMauiHybridRunner, MauiHybridRunner>();
        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
#if WITHBLAZOR
        builder.Services.AddSingleton<ComponentStatePersistenceManager>();
        builder.Services.AddScoped<PersistentComponentState>(sp =>
            sp.GetRequiredService<ComponentStatePersistenceManager>().State);

        builder.Services.AddRadzenComponents();
        builder.Services.AddMudServices();
        builder.Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);
        builder.Services.AddSyncfusionBlazor(); 
#endif
        //var inMemorySettings = new Dictionary<string, string?>
        //                            {
        //                                { "GeoBlazor:LicenseKey", Other.Internals.Licenses.GEOBLAZOR_LICENSE },
        //                                { "ArcGISApiKey", Other.Internals.Licenses.ARCGIS_API_KEY }
        //                            };
        //builder.Configuration.AddInMemoryCollection(inMemorySettings);
        //builder.Services.AddGeoBlazor(builder.Configuration);

        var host = builder.Build();
#if WITHBLAZOR
        //Make lazyLoader accessible:
        ServiceLocator.Provider = host.Services; 
#endif
        return host;
    }
}
