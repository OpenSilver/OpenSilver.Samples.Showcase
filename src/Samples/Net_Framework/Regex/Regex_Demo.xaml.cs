using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
    }
}
