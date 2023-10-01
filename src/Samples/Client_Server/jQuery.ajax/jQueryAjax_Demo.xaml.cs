using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace OpenSilver.Samples.Showcase
{
    public partial class jQueryAjax_Demo : UserControl
    {
        public jQueryAjax_Demo()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = await OpenSilver.Extensions.jQueryAjaxHelper.MakeAjaxCall(
                    url: "http://fiddle.jshell.net/echo/html/",
                    data: "some sample text",
                    type: "post");

                MessageBox.Show("The server returned the following result: " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQueryAjax_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQueryAjax_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQueryAjaxHelper.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQueryAjax_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.vb"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQueryAjaxHelper.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/vbsrc/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.vb"
                }
            });
        }
    }
}
