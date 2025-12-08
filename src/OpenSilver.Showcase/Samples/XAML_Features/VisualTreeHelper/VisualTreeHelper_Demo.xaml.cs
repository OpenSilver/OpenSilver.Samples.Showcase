using OpenSilver.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("logicaltreehelper", "UI elements", "XAML", "hierarchy", "UI", "treeview")]
public partial class VisualTreeHelper_Demo : UserControl
{
    public VisualTreeHelper_Demo()
    {
        InitializeComponent();
    }

    private void RevealTree_Click(object sender, RoutedEventArgs e)
    {
        var viewer = new TreeViewerWindow(this);
        viewer.Show();
    }
}
