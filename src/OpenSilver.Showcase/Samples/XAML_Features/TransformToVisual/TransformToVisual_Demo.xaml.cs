using OpenSilver.Showcase.Search;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenSilver.Showcase;

[SearchKeywords("position", "relative")]
public partial class TransformToVisual_Demo : UserControl
{
    public TransformToVisual_Demo()
    {
        InitializeComponent();

        Loaded += OnLoaded;
        Application.Current.MainWindow.SizeChanged += (_, _) => CalculatePosition();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        (VisualTreeHelper.GetParent(this) as FrameworkElement).LayoutUpdated += (_, _) => CalculatePosition();
    }

    private void CalculatePosition()
    {
        var transform = TransformToVisual(Application.Current.MainWindow);
        var topLeft = transform.Transform(new Point());

        resultTextBlock.Text = $"X: {topLeft.X:N1}  Y: {topLeft.Y:N1}";
    }
}
