using System.Collections.Generic;

#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif
namespace OpenSilver.Samples.Showcase
{
    public partial class PropertyChangedTrigger_Demo : UserControl
    {
        public PropertyChangedTrigger_Demo()
        {
            this.InitializeComponent(); 
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "PropertyChangedTrigger_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "PropertyChangedTrigger_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "PropertyChangedTrigger_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.vb"
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            borderText.Text = borderText.Text == "Yellow" ? "Orange" : "Yellow";
        }
    }
}