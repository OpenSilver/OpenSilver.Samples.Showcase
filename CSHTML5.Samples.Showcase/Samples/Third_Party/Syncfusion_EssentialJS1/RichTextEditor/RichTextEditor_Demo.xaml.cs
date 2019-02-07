using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Syncfusion_EssentialJS1.RichTextEditor
{
    public partial class RichTextEditor_Demo : Page
    {
        public RichTextEditor_Demo()
        {
            InitializeComponent();

            Loaded += RichTextEditor_Demo_Loaded;
        }

        private async void RichTextEditor_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until RichTextEditor's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            await RTE.JSInstanceLoaded;
            LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
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
