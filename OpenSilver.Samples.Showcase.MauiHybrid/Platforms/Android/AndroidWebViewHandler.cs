using Microsoft.AspNetCore.Components.WebView.Maui;

namespace OpenSilver.Samples.Showcase.MauiHybrid;

public class AndroidWebViewHandler : BlazorWebViewHandler
{
    protected override void ConnectHandler(global::Android.Webkit.WebView webView)
    {
        webView.Settings.SetSupportMultipleWindows(false);
        webView.Settings.TextZoom = 100; // Set text zoom to 100% to avoid scaling issues

        base.ConnectHandler(webView);
    }
}
