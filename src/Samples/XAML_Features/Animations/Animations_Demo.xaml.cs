using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Animations_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Animations_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml.cs"
                }
            });
        }

    }
}
