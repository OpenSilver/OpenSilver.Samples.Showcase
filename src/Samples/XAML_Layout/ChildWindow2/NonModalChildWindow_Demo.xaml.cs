using OpenSilver.Samples.Showcase.Search;
using PreviewOnWinRT;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("layout", "window", "popup", "nonmodal", "non-modal", "dialog")]
    public partial class NonModalChildWindow_Demo : UserControl
    {
        private int _n = 1;

        public NonModalChildWindow_Demo()
        {
            InitializeComponent();
        }

        private void ButtonTestChildWindow_Modal_Click(object sender, RoutedEventArgs e)
        {
            var childWindow = new SmallChildWindow
            {
                Title = $"ChildWindow (Modal) #{_n++}",
                IsModal = true
            };
            childWindow.Show();
        }

        private void ButtonTestChildWindow_NonModal_Click(object sender, RoutedEventArgs e)
        {
            var childWindow = new SmallChildWindow
            {
                Title = $"ChildWindow (Non-Modal) #{_n++}",
                IsModal = false,
            };
            childWindow.optionsStack.Visibility = Visibility.Collapsed;
            childWindow.Show();
        }
    }
}
