using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace CSHTML5.Samples.Showcase
{
    public partial class AsyncAwait_Demo : UserControl
    {
        public AsyncAwait_Demo()
        {
            this.InitializeComponent();
        }

        async void ButtonToDemonstrateAsyncAwait_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Visibility = Visibility.Collapsed;
            TaskBasedCounterTextBlock.Visibility = Visibility.Visible;
            TaskBasedCounterTextBlock.Text = "5 seconds left";
            await Task.Delay(1000);
            TaskBasedCounterTextBlock.Text = "4 seconds left";
            await Task.Delay(1000);
            TaskBasedCounterTextBlock.Text = "3 seconds left";
            await Task.Delay(1000);
            TaskBasedCounterTextBlock.Text = "2 seconds left";
            await Task.Delay(1000);
            TaskBasedCounterTextBlock.Text = "1 second left";
            await Task.Delay(1000);
            TaskBasedCounterTextBlock.Text = "Done!";
            TaskBasedCounterTextBlock.Visibility = Visibility.Collapsed;
            button.Visibility = Visibility.Visible;
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "AsyncAwait_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "AsyncAwait_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml.cs"
                }
            });
        }

    }
}
