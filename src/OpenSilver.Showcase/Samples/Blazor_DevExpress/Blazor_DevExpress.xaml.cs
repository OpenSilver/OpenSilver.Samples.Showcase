using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase;

public partial class Blazor_DevExpress : Page
{
    public Blazor_DevExpress()
    {
        InitializeComponent();
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
            baseUri = baseUri.EndsWith("/") ? baseUri : baseUri + "/";
            Interop.LoadCssFilesAsync(
                new string[]
                {
                    baseUri + "_content/DevExpress.Blazor.Themes/blazing-berry.bs5.min.css",
                    baseUri + "_content/DevExpress.Blazor.RichEdit/dx-blazor-richedit.css"
                },
                () =>
                {
                    //Add the page's content
                    Content = new DevExpress_Sample();
                }
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
#else
        Content = new WebBrowser { SourceUri = new Uri($"{BlazorHelper.FullAppBaseUri}Blazor_DevExpress?menu=hidden") };
#endif
    }
}
