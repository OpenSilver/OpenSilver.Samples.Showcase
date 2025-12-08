using OpenSilver.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("HTML", "interop", "js", "numeric", "colorpicker")]
public partial class Interop_HtmlPresenter_Demo : UserControl
{
    public Interop_HtmlPresenter_Demo()
    {
        InitializeComponent();
    }

    private void ButtonClickHere_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("The value is: " + NumericTextBox1.Value.ToString());
    }
}
