using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("drag", "scroll")]
public partial class Thumb_Demo : UserControl
{
    public Thumb_Demo()
    {
        InitializeComponent();
    }

    private void OnThumbDragStarted(object sender, DragStartedEventArgs e)
    {
        infoTextBlock.Text = $"DragStarted X: {e.HorizontalOffset}; Y: {e.VerticalOffset}";
        Cursor = Cursors.ScrollAll;
    }

    private void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        infoTextBlock.Text = $"DragDelta X: {e.HorizontalChange:N1}; Y: {e.VerticalChange:N1}";
    }

    private void OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
    {
        infoTextBlock.Text = $"DragCompleted X: {e.HorizontalChange:N1}; Y: {e.VerticalChange:N1}";
        Cursor = null;
    }
}
