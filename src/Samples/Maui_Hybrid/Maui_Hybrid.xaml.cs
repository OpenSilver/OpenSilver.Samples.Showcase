using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class Maui_Hybrid : Page
    {
        public Maui_Hybrid()
        {            
            //InitializeComponent();

            //Loaded += async (_, _) => await LazyLoadingHelper.LoadMauiAssemblies();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LazyLoadingHelper.LoadMauiAssemblies();
            InitializeComponent();
        }
    }
}
