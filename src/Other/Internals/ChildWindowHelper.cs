using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

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
