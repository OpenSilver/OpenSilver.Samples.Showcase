using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CSHTML5.Samples.Showcase
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
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "PropertyChangedTrigger_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/PropertyChangedTrigger/PropertyChangedTrigger_Demo.xaml.cs"
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            borderText.Text = borderText.Text == "Yellow" ? "Orange" : "Yellow";
        }
    }
}