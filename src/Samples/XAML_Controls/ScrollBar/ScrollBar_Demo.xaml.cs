using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
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
