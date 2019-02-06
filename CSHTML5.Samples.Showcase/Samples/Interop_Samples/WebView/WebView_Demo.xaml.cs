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
    public partial class WebView_Demo : UserControl
    {
        public WebView_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonNavigate_Click(object sender, RoutedEventArgs e)
        {
            //WebView1.Navigate(new Uri(TextBoxWithURL.Text));
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebView_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Interop_Samples/WebView/WebView_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "WebView_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Interop_Samples/WebView/WebView_Demo.xaml.cs"
                }
            });
        }
    }
}
