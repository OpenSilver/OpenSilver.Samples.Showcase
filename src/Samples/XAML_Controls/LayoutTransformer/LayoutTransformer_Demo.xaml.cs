using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("transformation", "scale", "rotate", "skew")]
public partial class LayoutTransformer_Demo : ContentControl
{
    public LayoutTransformer_Demo()
    {
        InitializeComponent();
    }

    private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        layoutTransformer.ApplyLayoutTransform();
    }
}
