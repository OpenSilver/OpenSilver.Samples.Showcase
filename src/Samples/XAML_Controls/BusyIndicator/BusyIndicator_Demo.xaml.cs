using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace OpenSilver.Samples.Showcase
{
    public partial class BusyIndicator_Demo : UserControl
    {
        DispatcherTimer timer;


        public BusyIndicator_Demo()
        {
            this.InitializeComponent();
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
