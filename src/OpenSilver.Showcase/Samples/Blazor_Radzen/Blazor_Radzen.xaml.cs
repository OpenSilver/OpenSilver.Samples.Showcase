using System;
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
        Content = new WebBrowser { SourceUri = new Uri($"{BlazorHelper.FullAppBaseUri}Blazor_Radzen?menu=hidden") };
#endif
    }
}
