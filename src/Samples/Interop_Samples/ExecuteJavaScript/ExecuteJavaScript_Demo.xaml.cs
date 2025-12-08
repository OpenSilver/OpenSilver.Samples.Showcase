using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("JavaScript", "interop", "browser", "script")]
    public partial class ExecuteJavaScript_Demo : UserControl
    {
        public ExecuteJavaScript_Demo()
        {
            this.InitializeComponent();
        }

        void SendJavaScriptMessage(object sender, RoutedEventArgs e)
        {
            Interop.ExecuteJavaScript("alert($0);", TextBoxInput.Text);
        }
    }
}
