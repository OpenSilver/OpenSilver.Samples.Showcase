using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase
{
    public partial class Blazor_MudBlazor : Page
    {
        public Blazor_MudBlazor()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if WITHBLAZOR
            Content = new MudBlazor_Sample();
#else
            Content = new WebBrowser { SourceUri = new Uri($"{BlazorHelper.FullAppBaseUri}Blazor_MudBlazor?menu=hidden") };
#endif
        }
    }
}
