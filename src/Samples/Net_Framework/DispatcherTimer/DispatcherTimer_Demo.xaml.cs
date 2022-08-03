using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
    public partial class DispatcherTimer_Demo : UserControl
    {
        DispatcherTimer _dispatcherTimer;

        public DispatcherTimer_Demo()
        {
            this.InitializeComponent();


            // Initialize the DispatcherTimer
            _dispatcherTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 100) };
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        void ButtonToStartTimer_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Start();
        }

        void ButtonToStopTimer_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Stop();
        }

        void DispatcherTimer_Tick(object sender, object e)
        {
            // Increment the counter by 1
            if (CounterTextBlock.Text == null || CounterTextBlock.Text == string.Empty)
                CounterTextBlock.Text = "0";
            else
                CounterTextBlock.Text = (int.Parse(CounterTextBlock.Text) + 1).ToString();
        }

        public void Dispose()
        {
            _dispatcherTimer.Stop();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DispatcherTimer_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DispatcherTimer_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml.cs"
                }
            });
        }

    }
}
