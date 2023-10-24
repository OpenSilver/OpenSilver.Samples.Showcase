using System;
using System.Windows;
using System.Windows.Controls;

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
    }
}
