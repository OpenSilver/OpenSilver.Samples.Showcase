using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Browser;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
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

namespace CSHTML5.Samples.Showcase
{
    public partial class HtmlPage_Demo : UserControl
    {
        public HtmlPage_Demo()
        {
            this.InitializeComponent();
        }

        void ButtonGetCurrentURL_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The current URL is: " + HtmlPage.Document.DocumentUri.OriginalString);
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "HtmlPage_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Net_Framework/HtmlPage/HtmlPage_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "HtmlPage_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Net_Framework/HtmlPage/HtmlPage_Demo.xaml.cs"
                }
            });
        }

    }
}
