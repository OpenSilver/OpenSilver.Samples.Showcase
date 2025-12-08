using OpenSilver.Samples.Showcase.Search;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("coordinates", "mouse", "hit test", "interaction")]
    public partial class FindElementsInHostCoordinates_Demo : UserControl
    {
        private int _highestZIndex;

        public FindElementsInHostCoordinates_Demo()
        {
            this.InitializeComponent();

            InitAllZIndex();
            _highestZIndex = 2;

            this.MouseLeftButtonDown += FindElementsInHostCoordinates_Demo_PointerPressed;
        }

        void FindElementsInHostCoordinates_Demo_PointerPressed(object sender, MouseButtonEventArgs e)
        {
            // Get the absolute coordinates of the pointer:
            Point currentPoint = e.GetPosition(null);

            // Find the element that is under the pointer:
            var uiElement = VisualTreeHelper.FindElementsInHostCoordinates(currentPoint, CanvasParent).FirstOrDefault();

            // Bring the clicked element to the front:
            if (uiElement is Border)
            {
                _highestZIndex++;
                Canvas.SetZIndex(uiElement, _highestZIndex);
            }
        }
        
        void InitAllZIndex()
        {
            // 0 -> 2 is from the background to the front
            Canvas.SetZIndex(BlueRectangle, 0);
            Canvas.SetZIndex(GreenRectangle, 1);
            Canvas.SetZIndex(YellowRectangle, 2);
        }
    }
}
