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
    public partial class GetDiv_Demo : UserControl
    {
        public GetDiv_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonSetCSS_Click(object sender, RoutedEventArgs e)
        {
            var div = Interop.GetDiv(this);

            Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div);
            
            // Note: refer to the documentation at: http://opensilver.net/links/how-to-call-javascript.aspx
        }
    }
}
