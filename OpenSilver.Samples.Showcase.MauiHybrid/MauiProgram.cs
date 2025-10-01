using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
//using dymaptic.GeoBlazor.Core;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using OpenSilver.MauiHybrid.Runner;
using Radzen;
using Syncfusion.Blazor;
using System.Globalization;

namespace OpenSilver.Samples.Showcase.MauiHybrid;

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

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Other.Internals.Licenses.SYNCFUSION_LICENSE);

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

        //For lazy loading
        builder.Services.AddScoped<LazyAssemblyLoader>();
        builder.Services.AddScoped<ILazyFeatureNavigator, LazyFeatureNavigator>();

        builder.Services.AddScoped<IMauiHybridRunner, MauiHybridRunner>();
        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
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
        //var inMemorySettings = new Dictionary<string, string?>
        //                            {
        //                                { "GeoBlazor:LicenseKey", Other.Internals.Licenses.GEOBLAZOR_LICENSE },
        //                                { "ArcGISApiKey", Other.Internals.Licenses.ARCGIS_API_KEY }
        //                            };
        //builder.Configuration.AddInMemoryCollection(inMemorySettings);
        //builder.Services.AddGeoBlazor(builder.Configuration);

        var host = builder.Build();
        //Make lazyLoader accessible:
        ServiceLocator.Provider = host.Services;
        return host;
    }
}
