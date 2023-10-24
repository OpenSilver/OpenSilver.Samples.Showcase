using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
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
    public partial class DateAndTimePicker_Demo : UserControl
    {
        public DateAndTimePicker_Demo()
        {
            this.InitializeComponent();
        }

        private void RegisterEvent_Click(object sender, RoutedEventArgs e)
        {
            if(TimePicker.Value != null && DatePicker.SelectedDate != null)
            {
                MessageBox.Show("Your event is confirmed at " + TimePicker.Value.Value.ToShortTimeString() + " on " + DatePicker.SelectedDate.Value.ToShortDateString());
            }
            else
            {
                MessageBox.Show("Please enter a Date and a Time");
            }
        }
    }
}
