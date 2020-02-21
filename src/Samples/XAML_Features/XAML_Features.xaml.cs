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
    public partial class Xaml_Features : UserControl
    {
        public Xaml_Features()
        {
            this.InitializeComponent();

#if OPENSILVER
            Binding1Demo.Visibility = Visibility.Collapsed;
#endif
        }
    }
}
