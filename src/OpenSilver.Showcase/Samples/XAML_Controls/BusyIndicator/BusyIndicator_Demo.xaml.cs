using OpenSilver.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OpenSilver.Showcase
{
    [SearchKeywords("loading", "busy", "indicator", "status", "loading", "progress", "style", "template")]
    public partial class BusyIndicator_Demo : UserControl
    {
        private readonly DispatcherTimer timer;

        public BusyIndicator_Demo()
        {
            InitializeComponent();
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 3) };
            timer.Tick += StopBusyIndicator;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyBusyIndicator.IsBusy = true;
            timer.Start();
        }

        private void StopBusyIndicator(object sender, EventArgs e)
        {
            timer.Stop();
            MyBusyIndicator.IsBusy = false;
        }
    }
}
