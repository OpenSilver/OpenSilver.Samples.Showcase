using System.Windows;
using System.Windows.Controls;

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
