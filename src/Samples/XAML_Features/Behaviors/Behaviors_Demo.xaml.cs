using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("behavior", "interaction", "draggable", "mousedrag", "mousedragelementbehavior", "fluidmove", "fluidmovebehavior", "datastatebehavior", "effects")]
    public partial class Behaviors_Demo : UserControl
    {
        public Behaviors_Demo()
        {
            InitializeComponent();
        }

        private void OnMoveLeftButtonClick(object sender, RoutedEventArgs e)
        {
            double currentLeft = Canvas.GetLeft(MovingRectangle);
            if (currentLeft > 10) // Prevent moving out of bounds
            {
                Canvas.SetLeft(MovingRectangle, currentLeft - 50);
            }
        }

        private void OnMoveRightButtonClick(object sender, RoutedEventArgs e)
        {
            double currentLeft = Canvas.GetLeft(MovingRectangle);
            if (currentLeft < 150) // Prevent moving out of bounds
            {
                Canvas.SetLeft(MovingRectangle, currentLeft + 50);
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            wrapPanel.Children.Insert(0, new Border());
            UpdateRemoveButtonState();
        }

        private void OnRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            if (wrapPanel.Children.Count > 0)
            {
                wrapPanel.Children.RemoveAt(0);
                UpdateRemoveButtonState();
            }
        }

        private void UpdateRemoveButtonState()
        {
            RemoveButton.IsEnabled = wrapPanel.Children.Count > 0;
        }
    }
}
