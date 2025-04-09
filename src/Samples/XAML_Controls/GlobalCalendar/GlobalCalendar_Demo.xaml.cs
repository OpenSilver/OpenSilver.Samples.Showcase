using OpenSilver.Samples.Showcase.Search;
using System.Globalization;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("input", "date", "selection", "schedule", "picker", "control", "culture", "globalization")]
public partial class GlobalCalendar_Demo : UserControl
{
    public GlobalCalendar_Demo()
    {
        InitializeComponent();
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
