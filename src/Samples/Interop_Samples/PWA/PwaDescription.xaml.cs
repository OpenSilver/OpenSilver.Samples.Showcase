using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class PwaDescription : UserControl
    {
        public PwaDescription()
        {
            InitializeComponent();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "index.html",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase.Browser/wwwroot/index.html"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "manifest.json",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase.Browser/wwwroot/manifest.json"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "service-worker.published.js",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase.Browser/wwwroot/service-worker.published.js"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "OpenSilver.Samples.Showcase.Browser.csproj",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/OpenSilver.Samples.Showcase.Browser/OpenSilver.Samples.Showcase.Browser.csproj"
                }
            });
        }
    }
}
