using System;
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
        Content = new WebBrowser { SourceUri = new Uri($"{BlazorHelper.FullAppBaseUri}Blazor_Blazorise?menu=hidden") };
#endif
    }
}
