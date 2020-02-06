using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class FullScreen_Demo : UserControl
    {
        public Host currentHost;
        public FullScreen_Demo()
        {
            this.InitializeComponent();
            currentHost = new Host();
        }

        public void EnterFullScreen_Click(object sender, RoutedEventArgs e)
        {
            currentHost.Content.IsFullScreen = true;
        }

        public void ExitFullSCreen_Click(object sender, RoutedEventArgs e)
        {
            currentHost.Content.IsFullScreen = false;
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "FullScreen_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/DispatcherTimer/FullScreen_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "FullScreen_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/DispatcherTimer/FullScreen_Demo.xaml.cs"
                }
            });
        }

    }
}
