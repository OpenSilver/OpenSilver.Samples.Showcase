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
    public partial class GetDiv_Demo : UserControl
    {
        public GetDiv_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonSetCSS_Click(object sender, RoutedEventArgs e)
        {
            var div = Interop.GetDiv(this);

            Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div);
            
            // Note: refer to the documentation at: http://cshtml5.com/links/how-to-call-javascript.aspx
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "GetDiv_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "GetDiv_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Interop_Samples/GetDiv/GetDiv_Demo.xaml.cs"
                }
            });
        }

    }
}
