using OpenSilver.Showcase.Search;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("input", "calendar", "date", "selection", "schedule", "picker", "control", "culture", "globalization")]
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

        private void OnCultureChanged(object sender, SelectionChangedEventArgs e)
        {
            if (culturesCombo.SelectedItem is not ComboBoxItem selected)
            {
                return;
            }

            var culture = new CultureInfo(selected.Tag as string);
            globalCalendar.CalendarInfo = new CultureCalendarInfo(culture);
        }
    }
}
