using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
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
    public partial class Xaml_Controls : UserControl
    {
        public Xaml_Controls()
        {
            this.InitializeComponent();

#if OPENSILVER
            NonModalChildWindow.Visibility = Visibility.Collapsed;
#endif
            ScrollBarDemo.Visibility = Visibility.Collapsed;
            ThumbDemo.Visibility = Visibility.Collapsed;
            FrameDemo.Visibility = Visibility.Collapsed; // The Showcase already uses a Frame to change pages anyway
        }
    }
}
