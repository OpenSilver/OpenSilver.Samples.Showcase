using OpenSilver.ControlsKit;
using OpenSilver.Showcase.Search;
using System.Collections.Generic;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("layout", "Panel", "container", "UI")]
    public partial class FlexPanel_Demo : UserControl
    {
        public FlexPanel_Demo()
        {
            this.InitializeComponent();

            List<JustifyContent> justifies = new List<JustifyContent> { JustifyContent.Start, JustifyContent.End, JustifyContent.Center, JustifyContent.SpaceAround, JustifyContent.SpaceBetween, JustifyContent.SpaceEvenly };
            JustifyComboBox.ItemsSource = justifies;
        }

    }
}
