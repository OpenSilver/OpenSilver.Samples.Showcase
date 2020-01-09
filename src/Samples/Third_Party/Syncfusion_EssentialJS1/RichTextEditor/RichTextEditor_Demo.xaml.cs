using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Syncfusion_EssentialJS1.RichTextEditor
{
    public partial class RichTextEditor_Demo : Page
    {
        public RichTextEditor_Demo()
        {
            InitializeComponent();

            //Note: Below is an example of setting the location for the required scripts and css for the Syncfusion EssentialJS1 RichTextEditor control. See the tutorial at http://cshtml5.com for more information.
            ej_rte.ej.RTE.Configuration.LocationOfEjRTEJS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Syncfusion_EssentialJS1/scripts/ej.rte.min.js";
            ej_rte.ej.RTE.Configuration.LocationOfEjWebAllCss = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Syncfusion_EssentialJS1/themes/default-theme/ej.web.all.min.css";

            Loaded += RichTextEditor_Demo_Loaded;
        }

        private async void RichTextEditor_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until RichTextEditor's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            bool result = await RTE.JSInstanceLoaded;
            if (result)
            {
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "RichTextEditor_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Syncfusion_EssentialJS1/RichTextEditor/RichTextEditor_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "RichTextEditor_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Syncfusion_EssentialJS1/RichTextEditor/RichTextEditor_Demo.xaml.cs"
                }
            });
        }
    }
}
