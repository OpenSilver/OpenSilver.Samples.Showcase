using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("fullscreen", "UI", "display", "window", "maximize")]
    public partial class FullScreen_Demo : UserControl
    {
        public FullScreen_Demo()
        {
            this.InitializeComponent();
        }

        public void EnterFullScreen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = true;
        }

        public void ExitFullSCreen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = false;
        }
    }
}
