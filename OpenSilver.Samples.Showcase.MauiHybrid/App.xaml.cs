using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace OpenSilver.Samples.Showcase.MauiHybrid
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var mainPage = new MainPage();
            mainPage.On<iOS>().SetUseSafeArea(true); // prevent overlapping the status bar
            return new Window(mainPage) { Title = "OpenSilver Showcase" };
        }
    }
}
