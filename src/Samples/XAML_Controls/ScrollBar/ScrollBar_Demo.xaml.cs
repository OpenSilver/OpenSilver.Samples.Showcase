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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ScrollBar_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ScrollBar_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ScrollBar_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml.vb"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DefaultScrollBarStyle.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/Styles/DefaultScrollBarStyle.xaml"
                }
            });
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            TextDisplay.Text = e.NewValue.ToString("0.000");
        }

    }
}
