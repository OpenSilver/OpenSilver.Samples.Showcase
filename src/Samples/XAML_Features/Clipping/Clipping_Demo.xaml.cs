using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("clip")]
public partial class Clipping_Demo : UserControl
{
    public Clipping_Demo()
    {
        InitializeComponent();

        clipCheckBox.Checked += OnClipCheckBoxStateChanged;
        clipCheckBox.Unchecked += OnClipCheckBoxStateChanged;
    }

    private void OnClipCheckBoxStateChanged(object sender, RoutedEventArgs e)
    {
        if (clipCheckBox.IsChecked == true)
        {
            button.Clip = new EllipseGeometry
            {
                Center = new Point(70, 30),
                RadiusX = 70,
                RadiusY = 30
            };
        }
        else
        {
            button.Clip = null;
        }
    }
}
