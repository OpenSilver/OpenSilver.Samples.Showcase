using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using OpenSilver.MauiHybrid.Runner;
using Radzen;
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


        return builder.Build();
    }
}
