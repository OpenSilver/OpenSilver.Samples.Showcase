using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("updown")]
public partial class ButtonSpinner_Demo : ContentControl
{
    private readonly string[] _months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    private int _monthIndex = 0;

    public ButtonSpinner_Demo()
    {
        InitializeComponent();

        UpdateSpinnerContent();
    }

    private void OnButtonSpinnerSpin(object sender, SpinEventArgs e)
    {
        _monthIndex = e.Direction == SpinDirection.Increase ? _monthIndex + 1 : _monthIndex - 1;

        if (_monthIndex < 0)
        {
            _monthIndex = _months.Length - 1;
        }
        else if (_monthIndex >= _months.Length)
        {
            _monthIndex = 0;
        }

        UpdateSpinnerContent();
    }

    private void UpdateSpinnerContent()
    {
        spinner.Content = _months[_monthIndex];
    }
}
