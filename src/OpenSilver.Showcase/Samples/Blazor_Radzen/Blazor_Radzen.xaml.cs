using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase;

public partial class Blazor_Radzen : Page
{
    public Blazor_Radzen()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
#if WITHBLAZOR
        Content = new Radzen_Sample();
#else
        BlazorHelper.NavigateTo(nameof(Blazor_Radzen));
#endif
    }

#if !WITHBLAZOR
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
}
