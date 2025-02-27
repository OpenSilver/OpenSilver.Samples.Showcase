using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("animation", "effects", "motion", "behavior")]
    public partial class Animations_Demo : UserControl
    {
        public Animations_Demo()
        {
            this.InitializeComponent();
        }

        void ButtonToStartAnimationOpen_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)CanvasForAnimationsDemo.Resources["AnimationToOpen"];
            storyboard.Begin();
            ButtonToStartAnimationOpen.Visibility = Visibility.Collapsed;
            ButtonToStartAnimationClose.Visibility = Visibility.Visible;
        }

        void ButtonToStartAnimationClose_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)CanvasForAnimationsDemo.Resources["AnimationToClose"];
            storyboard.Begin();
            ButtonToStartAnimationOpen.Visibility = Visibility.Visible;
            ButtonToStartAnimationClose.Visibility = Visibility.Collapsed;
        }
    }
}
