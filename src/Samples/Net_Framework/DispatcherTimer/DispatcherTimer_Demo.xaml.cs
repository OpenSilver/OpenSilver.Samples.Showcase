using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
    }
}
