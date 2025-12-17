#if FULLBLAZOR
using DevExpress.Blazor;
#endif
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase;

public partial class Blazor_DevExpress : Page
{
    public Blazor_DevExpress()
    {
        InitializeComponent();

#if !FULLBLAZOR
        Content = null;
#endif
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
#if FULLBLAZOR
        try
        {
            //Load DevExpress dlls
            var nav = ServiceLocator.Get<ILazyFeatureNavigator>();
            await nav.EnsureLoadedFromPathAsync(LazyLoadingConstants.DEVEXPRESS_NAME);

            //// Load required js and css files:
            var baseUri = Interop.ExecuteJavaScript("document.baseURI").ToString();
            baseUri = baseUri.EndsWith('/') ? baseUri : baseUri + "/";
            Interop.LoadCssFilesAsync(
                [
                    baseUri + "_content/DevExpress.Blazor.Themes/blazing-berry.bs5.min.css",
                    baseUri + "_content/DevExpress.Blazor.RichEdit/dx-blazor-richedit.css"
                ],
                () =>
                {
                    //Add the page's content
                    Content = new DevExpress_Sample();
                    _isInitialized = true;
                    ServiceLocator.Get<IThemeChangeService>().SetTheme(CurrentTheme);
                }
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
#else
        BlazorHelper.NavigateTo(nameof(Blazor_DevExpress));
#endif
    }

#if FULLBLAZOR
    private static bool _isInitialized = false;
    private static bool _isDarkMode = false;
    private static DxTheme CurrentTheme => _isDarkMode ? DevExpress.Blazor.Themes.BlazingDark : DevExpress.Blazor.Themes.BlazingBerry;

    public static void UpdateTheme(bool isDarkMode)
    {
        _isDarkMode = isDarkMode;

        if (_isInitialized)
        {
            ServiceLocator.Get<IThemeChangeService>().SetTheme(CurrentTheme);
        }
    }
#else
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
}
