using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "calendar", "date", "selection", "schedule", "picker", "control")]
    public partial class Calendar_Demo : UserControl
    {
        public Calendar_Demo()
        {
            InitializeComponent();
        }

        private void OnPastDatesChanged(object sender, RoutedEventArgs e)
        {
            if (sampleCalendar == null)
            {
                return;
            }

            if ((bool)chkPastDateSelection.IsChecked)
            {
                sampleCalendar.BlackoutDates.Clear();
            }
            else
            {
                try
                {
                    sampleCalendar.BlackoutDates.AddDatesInPast();
                }
                catch
                {
                    chkPastDateSelection.IsChecked = true;
                }
            }
        }
    }
}
