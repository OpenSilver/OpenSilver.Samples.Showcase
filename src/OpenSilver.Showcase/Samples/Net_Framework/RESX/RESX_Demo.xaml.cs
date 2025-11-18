using OpenSilver.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("resources", "localization", "RESX", "translation")]
public partial class RESX_Demo : UserControl
{
    public RESX_Demo()
    {
        InitializeComponent();
    }

    private void ButtonReadResource_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show($"SampleResourceFile.InfoMessage: {SampleResourceFile.InfoMessage}");
    }

    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
        MainPage.Current.PageContainer.Navigate(new Uri("/XAML_Features/MarkupExtensions", UriKind.Relative));
    }
}
