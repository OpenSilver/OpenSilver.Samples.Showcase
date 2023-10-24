using System.Windows.Controls;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase
{
    public partial class MouseWheel_Demo : UserControl
    {
        public MouseWheel_Demo()
        {
            InitializeComponent();

            TitleContentControl.Content = "MouseWheel Event";
            ScrollBorder.MouseWheel += CountTotalScrollingDistance;
        }

        int scrollingDistance = 0;

        public void CountTotalScrollingDistance(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            int delta = e.Delta;

            scrollingDistance += delta;
            ScrollingDistanceTextBlock.Text = "Distance scrolled (with the mouse) on the border below: " + scrollingDistance + ".";
        }
    }
}
