using System.Windows;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase
{
    public sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Startup += OnAppStartup;
        }

        private async void OnAppStartup(object sender, StartupEventArgs e)
        {
            await FontFamily.LoadFontAsync("ms-appx:///OpenSilver.Samples.Showcase/Other/Inter_VariableFont_slnt_wght.ttf");

            Features.DOM.AssignClass = true;
            await Interop.LoadCssFile("ms-appx:///OpenSilver.Samples.Showcase/Other/CSS/app-styles.css");

            var mainPage = new MainPage();
            Window.Current.Content = mainPage;
        }
    }
}
