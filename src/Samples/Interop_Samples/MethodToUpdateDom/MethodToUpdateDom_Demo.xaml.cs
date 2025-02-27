using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("DOM", "JavaScript", "update", "HTML", "interop")]
    public partial class MethodToUpdateDom_Demo : UserControl
    {
        public MethodToUpdateDom_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonSetCSS_Click(object sender, RoutedEventArgs e)
        {
            var div = Interop.GetDiv(this);

            Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div);

            // Note: refer to the documentation at: https://doc.opensilver.net/documentation/in-depth-topics/call-javascript-from-csharp.html
        }
    }
}
