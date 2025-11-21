using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Showcase.Samples.Xaml3D;

public partial class Controls3D : Page
{
    public Controls3D()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        await Initializer.InitializeXaml3D();
        Dispatcher.BeginInvoke(() => Content = new Showcase.Xaml3D.Samples.Controls());
    }
}
