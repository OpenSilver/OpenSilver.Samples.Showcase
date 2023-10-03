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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DateAndTimePicker_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DateAndTimePickerd_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DateAndTimePickerd_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.vb"
                }

            });
        }

    }
}
