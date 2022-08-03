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
    public partial class HyperlinkButton_Demo : UserControl
    {
        public HyperlinkButton_Demo()
        {
            this.InitializeComponent();

#if OPENSILVER
            HyperlinkButtonDemo.NavigateUri = new Uri("http://www.opensilver.net"); 
    #endif

        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "HyperlinkButton.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/HyperlinkButton/HyperlinkButton.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "HyperlinkButton.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/HyperlinkButton/HyperlinkButton.xaml.cs"
                }
            });
        }

    }
}
