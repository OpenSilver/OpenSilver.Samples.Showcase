using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("HTML", "DOM", "JavaScript", "interop")]
    public partial class GetDiv_Demo : UserControl
    {
        public GetDiv_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonSetCSS_Click(object sender, RoutedEventArgs e)
        {
            var div = Interop.GetDiv(text);

            Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div);

            // Note: refer to the documentation at: https://doc.opensilver.net/documentation/in-depth-topics/call-javascript-from-csharp.html
        }
    }
}
