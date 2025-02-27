using OpenSilver.Samples.Showcase.Search;
using PreviewOnWinRT;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("layout", "window", "popup", "nonmodal", "non-modal", "dialog")]
    public partial class NonModalChildWindow_Demo : UserControl
    {
        int _n;
        public NonModalChildWindow_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonTestChildWindow_Modal_Click(object sender, RoutedEventArgs e)
        {
            SmallChildWindow childWindow = new SmallChildWindow();
            childWindow.Title = "ChildWindow (Modal)" + _n++;
#if !OPENSILVER
            childWindow.IsModal = true; 
#endif
            childWindow.Show();
        }
        private void ButtonTestChildWindow_NonModal_Click(object sender, RoutedEventArgs e)
        {
            SmallChildWindow childWindow = new SmallChildWindow();
            childWindow.Title = "ChildWindow (Non-modal)" + _n++;
#if !OPENSILVER
            childWindow.IsModal = false; 
#endif
            childWindow.Show();
        }
    }
}
