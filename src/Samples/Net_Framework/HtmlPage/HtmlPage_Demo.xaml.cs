using System.Windows.Browser;
using System.Windows;
using System.Windows.Controls;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("HTML", "browser", "web", "host")]
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
    }
}
