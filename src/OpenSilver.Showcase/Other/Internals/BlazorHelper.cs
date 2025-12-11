using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

#if !FULLBLAZOR
public static class BlazorHelper
{
    private static WebBrowser _webBrowser;

    public static string FullAppBaseUri { get; private set; }

    public static void Initialize(WebBrowser webBrowser, string fullAppBaseUri = null)
    {
        _webBrowser = webBrowser;
        FullAppBaseUri = fullAppBaseUri ?? $"{HtmlPage.Document.DocumentUri.GetLeftPart(UriPartial.Authority)}/full/#/";
    }

    public static void NavigateTo(string relativeUri, bool hidden = false)
    {
        if (_webBrowser == null)
        {
            throw new InvalidOperationException("BlazorHelper is not initialized. Call BlazorHelper.Initialize() first.");
        }
        _webBrowser.SourceUri = new Uri($"{FullAppBaseUri}{relativeUri}?menu=hidden");

        if (!hidden)
        {
            _webBrowser.Visibility = Visibility.Visible;
        }
    }

    public static void OnNavigatingFrom(Uri toUri)
    {
        if (_webBrowser == null)
        {
            throw new InvalidOperationException("BlazorHelper is not initialized. Call BlazorHelper.Initialize() first.");
        }

        if (!toUri.OriginalString.Contains("Blazor"))
        {
            _webBrowser.Visibility = Visibility.Collapsed;

            // clean up the iframe after a delay if not used, to free resources
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                if (_webBrowser.Visibility == Visibility.Collapsed)
                {
                    _webBrowser.SourceUri = null;
                }
            });
        }
    }
}
#endif
