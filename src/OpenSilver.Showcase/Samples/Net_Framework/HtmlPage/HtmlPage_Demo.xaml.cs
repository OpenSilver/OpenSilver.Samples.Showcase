using OpenSilver.Showcase.Search;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("HTML", "browser", "web", "host", "useragent", "platform")]
public partial class HtmlPage_Demo : UserControl
{
    public HtmlPage_Demo()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        documentUriTextBlock.Text = HtmlPage.Document.DocumentUri.OriginalString;
    }
}
