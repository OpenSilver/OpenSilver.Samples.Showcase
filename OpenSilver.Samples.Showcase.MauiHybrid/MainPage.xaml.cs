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
        }

        private async void OnBlazorWebViewUrlLoading(object? sender, UrlLoadingEventArgs e)
        {
            // if it is external link, WebKit does not handle it, so try to open it in Safari
            if (e.UrlLoadingStrategy == UrlLoadingStrategy.OpenExternally)
            {
                await Launcher.TryOpenAsync(e.Url);
            }
        }
    }
}
