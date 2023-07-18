using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class MouseWheel_Demo : UserControl
    {
        public MouseWheel_Demo()
        {
            InitializeComponent();

#if SLMIGRATION
            TitleContentControl.Content = "MouseWheel Event";
            ScrollBorder.MouseWheel += CountTotalScrollingDistance;
#else
            TitleContentControl.Content = "PointerWheelChanged Event";
            ScrollBorder.PointerWheelChanged += CountTotalScrollingDistance;
#endif
        }

        int scrollingDistance = 0;

#if SLMIGRATION
        public void CountTotalScrollingDistance(object sender, MouseWheelEventArgs e)
#else
        public void CountTotalScrollingDistance(object sender, PointerRoutedEventArgs e)
#endif
        {
            e.Handled = true;

#if SLMIGRATION
            int delta = e.Delta;
#else
            int delta = e.GetCurrentPoint(null).Properties.MouseWheelDelta;
#endif

            scrollingDistance += delta;
            ScrollingDistanceTextBlock.Text = "Distance scrolled (with the mouse) on the border below: " + scrollingDistance + ".";
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "MouseWheel_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "MouseWheel_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.cs"
                }
            });
        }
    }
}
