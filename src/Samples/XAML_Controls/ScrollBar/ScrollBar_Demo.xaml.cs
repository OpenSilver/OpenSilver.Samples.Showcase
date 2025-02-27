using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("scroll", "bar", "navigation", "UI", "container")]
    public partial class ScrollBar_Demo : UserControl
    {
        public ScrollBar_Demo()
        {
            this.InitializeComponent();

            TextDisplay.Text = Scrollbar.Value.ToString("0.000");
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            TextDisplay.Text = e.NewValue.ToString("0.000");
        }

    }
}
