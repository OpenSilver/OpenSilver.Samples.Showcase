using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

namespace CSHTML5.Samples.Showcase
{
    public partial class Keyboard_Demo : UserControl
    {
        public Keyboard_Demo()
        {
            this.InitializeComponent();
        }

#if SLMIGRATION
        private void TextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
#else
        private void TextBoxInput_KeyDown(object sender, KeyRoutedEventArgs e) 
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
#endif
                MessageBox.Show("You pressed Enter!" + Environment.NewLine + Environment.NewLine + "This is the text that you entered: " + TextBoxInput.Text);
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Keyboard_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Net_Framework/Keyboard/Keyboard_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Keyboard_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Net_Framework/Keyboard/Keyboard_Demo.xaml.cs"
                }
            });
        }

    }
}
