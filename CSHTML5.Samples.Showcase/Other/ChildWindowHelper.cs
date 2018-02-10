using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Samples.Showcase
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
