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
            var baseUri = Interop.ExecuteJavaScript("document.baseURI").ToString();
            var url = (baseUri.EndsWith("/") ? baseUri : baseUri + "/") +
                      "_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js";
            await Interop.LoadJavaScriptFile(url);
            url = (baseUri.EndsWith("/") ? baseUri : baseUri + "/") +
                      "_content/Syncfusion.Blazor.Themes/bootstrap5.css";
            await Interop.LoadCssFile(url);

            //Load Syncfusion dlls
            var nav = ServiceLocator.Get<ILazyFeatureNavigator>();
            await nav.EnsureLoadedFromPathAsync(LazyLoadingConstants.SYNCFUSION_NAME);
            //await nav.NavigateToAsync(LazyLoadingConstants.SYNCFUSION_NAME);

            //Add the page's content
            Content = new Syncfusion_Sample();
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

#if !FULLBLAZOR
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
}
