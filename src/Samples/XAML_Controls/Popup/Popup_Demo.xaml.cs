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
    public partial class Popup_Demo : UserControl
    {
        public Popup_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Popup_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Popup/Popup_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Popup_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Popup/Popup_Demo.xaml.cs"
                }
            });
        }

        private void OpenPopupButton_Click(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = true;
        }

        private void PopupButtonClose_Click(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = false;
        }
    }
}
