using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    static class ChildWindowHelper
    {
        public static void ShowChildWindow(UIElement content)
        {
            var childWindow = new ChildWindow()
            {
                Content = content,
                Style = (Style)App.Current.Resources["ViewMoreChildWindow_Style"]
            };
            childWindow.Show();
        }
    }
}
