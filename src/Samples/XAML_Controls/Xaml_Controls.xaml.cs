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

namespace CSHTML5.Samples.Showcase
{
    public partial class Xaml_Controls : UserControl
    {
        public Xaml_Controls()
        {
            this.InitializeComponent();

#if OPENSILVER
            //ScrollViewerDemo.Visibility = Visibility.Collapsed;
            //ComboBoxDemo.Visibility = Visibility.Collapsed;
            //ListBoxDemo.Visibility = Visibility.Collapsed;
            MenuItemDemo.Visibility = Visibility.Collapsed;
            ContextMenuDemo.Visibility = Visibility.Collapsed;
            //ImageDemo.Visibility = Visibility.Collapsed;
            //MediaElementDemo.Visibility = Visibility.Collapsed;
            //DataGridDemo.Visibility = Visibility.Collapsed;
            //DateAndTimePickerDemo.Visibility = Visibility.Collapsed;
            NonModalChildWindow.Visibility = Visibility.Collapsed;
#endif
            ScrollBarDemo.Visibility = Visibility.Collapsed;
            ThumbDemo.Visibility = Visibility.Collapsed;
            FrameDemo.Visibility = Visibility.Collapsed; // The Showcase already uses a Frame to change pages anyway
        }
    }
}
