using System.Windows.Controls;
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
        //BlazorHelper.NavigateTo(nameof(Blazor_GeoBlazor));
#endif
    }

#if !WITHBLAZOR
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
}
