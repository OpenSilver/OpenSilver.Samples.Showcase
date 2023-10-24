using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
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
    }
}
