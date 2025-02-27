using OpenSilver.Samples.Showcase.Search;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("regular expressions", "pattern", "match", "string processing", "search", "validation")]
    public partial class Regex_Demo : UserControl
    {
        public Regex_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonReplaceDates_Click(object sender, RoutedEventArgs e)
        {
            TextBlockOutputOfRegexReplaceDemo.Text = Regex.Replace(TextBoxRegexReplaceDemo.Text, @"(\d{2})/(\d{2})/(\d{2,4})", "$3-$2-$1");
        }
    }
}
