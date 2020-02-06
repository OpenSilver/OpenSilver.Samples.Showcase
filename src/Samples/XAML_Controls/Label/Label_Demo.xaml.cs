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
    public partial class Label_Demo : UserControl
    {
        public Label_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonViewMore_Click(object sender, RoutedEventArgs e)
        {
            //ChildWindowHelper.ShowChildWindow(new Button_Demo_More());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button!");
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Label_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/Label/Label_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Label_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/Label/Label_Demo.xaml.cs"
                },
            });
        }

    }
}
