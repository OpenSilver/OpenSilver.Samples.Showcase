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
    private static Func<bool> _isDarkTheme;

    public static string FullAppBaseUri { get; private set; }

    public static void Initialize(WebBrowser webBrowser, Func<bool> isDarkTheme, string fullAppBaseUri = null)
    {
        _webBrowser = webBrowser;
        _isDarkTheme = isDarkTheme;
        FullAppBaseUri = fullAppBaseUri ?? $"{HtmlPage.Document.DocumentUri.GetLeftPart(UriPartial.Authority)}/full/#/";
    }

    public static void NavigateTo(string relativeUri, bool hidden = false)
    {
        ValidateBrowser();

        _webBrowser.SourceUri = new Uri($"{FullAppBaseUri}{relativeUri}?menu=hidden&theme={(_isDarkTheme() ? "dark" : "light")}");

        if (!hidden)
        {
            _webBrowser.Visibility = Visibility.Visible;
        }
    }

    public static void OnNavigatingFrom(Uri toUri)
    {
        ValidateBrowser();

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

    public static void UpdateTheme()
    {
        ValidateBrowser();

        if (_webBrowser.Visibility == Visibility.Visible)
        {
            var uri = _webBrowser.SourceUri.OriginalString;
            _webBrowser.SourceUri = new Uri($"{uri[..uri.IndexOf('?')]}?menu=hidden&theme={(_isDarkTheme() ? "dark" : "light")}");
        }
    }

    private static void ValidateBrowser()
    {
        if (_webBrowser == null)
        {
            throw new InvalidOperationException("BlazorHelper is not initialized. Call BlazorHelper.Initialize() first.");
        }
    }
}
#endif
