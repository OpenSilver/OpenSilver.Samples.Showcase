using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class Grid_Demo : UserControl
    {
        public Grid_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonViewMore_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowHelper.ShowChildWindow(new Grid_Demo_More());
        }
    }
}
