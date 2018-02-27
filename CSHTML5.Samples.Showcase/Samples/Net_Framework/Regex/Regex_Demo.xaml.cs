using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
    public partial class Regex_Demo : UserControl
    {
        public Regex_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonReplaceDates_Click(object sender, RoutedEventArgs e)
        {
            TextBlockOutputOfRegexReplaceDemo.Text = Regex.Replace(TextBoxRegexReplaceDemo.Text, @"(\d{2})/(\d{2})/(\d{2,4})", "$3-$2-$1");
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Regex_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Regex/Regex_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Regex_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/Regex/Regex_Demo.xaml.cs"
                }
            });
        }

    }
}
