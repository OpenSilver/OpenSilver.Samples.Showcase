using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase;

public partial class Blazor_Blazorise : Page
{
    public Blazor_Blazorise()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
#if WITHBLAZOR
        Content = new Blazorise_Sample();
#else
        BlazorHelper.NavigateTo(nameof(Blazor_Blazorise));
#endif
    }

#if !WITHBLAZOR
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
}
