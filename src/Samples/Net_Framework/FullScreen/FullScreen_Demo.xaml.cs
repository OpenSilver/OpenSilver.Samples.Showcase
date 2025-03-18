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

            if (Interop.IsRunningInTheSimulator) // simulator or MAUI Hybrid
            {
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(new TextBlock
                {
                    Text = "This feature is not supported on the current platform.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 12,
                });
            }
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
