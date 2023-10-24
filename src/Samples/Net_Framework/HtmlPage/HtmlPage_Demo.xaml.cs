using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Browser;
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
    public partial class HtmlPage_Demo : UserControl
    {
        public HtmlPage_Demo()
        {
            this.InitializeComponent();
        }

        void ButtonGetCurrentURL_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The current URL is: " + HtmlPage.Document.DocumentUri.OriginalString);
        }
    }
}
