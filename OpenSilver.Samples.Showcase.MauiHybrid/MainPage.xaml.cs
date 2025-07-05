using Microsoft.AspNetCore.Components.WebView;

namespace OpenSilver.Samples.Showcase.MauiHybrid
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

#if IOS || MACCATALYST
            blazorWebView.UrlLoading += OnBlazorWebViewUrlLoading;
#endif
            Blazor.Initializer.UseBlazorForOpenSilver(blazorWebView.RootComponents);
        }

        private async void OnBlazorWebViewUrlLoading(object? sender, UrlLoadingEventArgs e)
        {
            if (e.Url != null &&
                (string.Equals(e.Url.AbsoluteUri, "about:blank", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(e.Url.AbsoluteUri, "about:srcdoc", StringComparison.OrdinalIgnoreCase) ||
                e.Url.AbsoluteUri.StartsWith("https://www.youtube.com/", StringComparison.OrdinalIgnoreCase)))
            {
                e.UrlLoadingStrategy = UrlLoadingStrategy.OpenInWebView;
            }

            // if it is external link, WebKit does not handle it, so try to open it in Safari
            if (e.UrlLoadingStrategy == UrlLoadingStrategy.OpenExternally && e.Url != null)
            {
                await Launcher.TryOpenAsync(e.Url);
            }
        }
    }
}
