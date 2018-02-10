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
    }
}
