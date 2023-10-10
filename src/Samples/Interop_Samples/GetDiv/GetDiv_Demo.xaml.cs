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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "GetDiv_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ExecuteJavaScript_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ExecuteJavaScript_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/ExecuteJavaScript/ExecuteJavaScript_Demo.xaml.vb"
                }
            });
        }

    }
}
