using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("input", "selection", "range")]
public partial class TimePicker_Demo : ContentControl
{
    public TimePicker_Demo()
    {
        InitializeComponent();

        popupModes.ItemsSource = new[]
        {
            new PopupMode { Name = "List", Popup = timePicker.Popup },
            new PopupMode { Name = "Range", Popup = new RangeTimePickerPopup() }
        };
    }

    private class PopupMode
    {
        public string Name { get; set; }

        public TimePickerPopup Popup { get; set; }
    }
}
