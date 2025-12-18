using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase;

public partial class Blazor_Syncfusion : Page
{
    public Blazor_Syncfusion()
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
            // Load required js and css files:
            await Interop.LoadJavaScriptFile($"~/_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js");
            await Interop.LoadCssFile($"~/_content/Syncfusion.Blazor.Themes/{CurrentTheme}");

            Interop.ExecuteJavaScriptVoid("document.querySelector(\"head link[href*='Syncfusion.Blazor.Themes']\").id = 'syncfusionThemeLink'");

            if (!Interop.IsRunningInTheSimulator)
            {
                //Load Syncfusion dlls
                var nav = ServiceLocator.Get<ILazyFeatureNavigator>();
                await nav.EnsureLoadedFromPathAsync(LazyLoadingConstants.SYNCFUSION_NAME);
            }

            Content = new Syncfusion_Sample();
            _isInitialized = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
#else
        BlazorHelper.NavigateTo(nameof(Blazor_Syncfusion));
#endif
    }

#if FULLBLAZOR
    private static bool _isInitialized = false;
    private static bool _isDarkMode = false;
    private static string CurrentTheme => _isDarkMode ? "bootstrap5-dark.css" : "bootstrap5.css";

    public static void UpdateTheme(bool isDarkMode)
    {
        _isDarkMode = isDarkMode;

        if (_isInitialized)
        {
            Interop.ExecuteJavaScriptVoid(
                $"document.getElementById('syncfusionThemeLink').setAttribute('href', '_content/Syncfusion.Blazor.Themes/{CurrentTheme}')");
        }
    }
#else
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
}
