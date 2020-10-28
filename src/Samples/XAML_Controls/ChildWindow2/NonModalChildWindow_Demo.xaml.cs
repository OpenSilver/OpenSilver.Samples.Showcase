using PreviewOnWinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class NonModalChildWindow_Demo : UserControl
    {
        int _n;
        public NonModalChildWindow_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonTestChildWindow_Modal_Click(object sender, RoutedEventArgs e)
        {
            SmallChildWindow childWindow = new SmallChildWindow();
            childWindow.Title = "ChildWindow (Modal)" + _n++;
            childWindow.IsModal = true;
            childWindow.Show();
        }
        private void ButtonTestChildWindow_NonModal_Click(object sender, RoutedEventArgs e)
        {
            SmallChildWindow childWindow = new SmallChildWindow();
            childWindow.Title = "ChildWindow (Non-modal)" + _n++;
            childWindow.IsModal = false;
            childWindow.Show();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "NonModalChildWindow_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "NonModalChildWindow_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/NonModalChildWindow_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "SmallChildWindow.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "SmallChildWindow.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow2/SmallChildWindow.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DefaultChildWindowStyle.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ChildWindow/Styles/DefaultChildWindowStyle.xaml"
                }
            });
        }
    }
}
