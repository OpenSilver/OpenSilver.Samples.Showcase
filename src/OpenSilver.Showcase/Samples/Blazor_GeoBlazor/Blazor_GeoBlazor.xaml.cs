using System;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace OpenSilver.Showcase;

public partial class Blazor_GeoBlazor : Page
{
    public Blazor_GeoBlazor()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
#if FULLBLAZOR
        //Content = new GeoBlazor_Sample();
#else
        //Content = new WebBrowser { SourceUri = new Uri($"{BlazorHelper.FullAppBaseUri}Blazor_GeoBlazor?menu=hidden") };
#endif
    }
}
