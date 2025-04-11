using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("interaction", "interactivity", "triggers", "behavior", "events", "UI")]
public partial class PropertyChangedTrigger_Demo : UserControl
{
    public PropertyChangedTrigger_Demo()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        borderText.Text = borderText.Text == "Yellow" ? "Orange" : "Yellow";
    }
}