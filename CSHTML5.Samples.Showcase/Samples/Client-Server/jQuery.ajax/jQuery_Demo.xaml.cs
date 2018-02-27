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
    public partial class jQuery_Demo : UserControl
    {
        public jQuery_Demo()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = await CSHTML5.Extensions.jQueryAjaxHelper.MakeAjaxCall(
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
                    TabHeader = "jQuery_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Client-Server/jQuery.ajax/jQuery_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQuery_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Client-Server/jQuery.ajax/jQuery_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "jQueryAjaxHelper.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Client-Server/jQuery.ajax/jQueryAjaxHelper.cs"
                }
            });
        }
    }
}
