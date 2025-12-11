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
            BlazorHelper.NavigateTo(nameof(Blazor_MudBlazor));
#endif
        }

#if !WITHBLAZOR
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
            => BlazorHelper.OnNavigatingFrom(e.Uri);
#endif
    }
}
