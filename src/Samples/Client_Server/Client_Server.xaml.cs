using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
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
    public partial class Client_Server : UserControl
    {
        public Client_Server()
        {
            this.InitializeComponent();

#if OPENSILVER
            REST_WebClientDemo.Visibility = System.Windows.Visibility.Collapsed;
#endif
        }
    }
}
