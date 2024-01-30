namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type DateAndTimePicker_Demo() as this =
    inherit DateAndTimePicker_DemoXaml()

    do
        this.InitializeComponent()

    member private this.RegisterEvent_Click(sender : obj, e : RoutedEventArgs) =
        if this.TimePicker.Value.HasValue && this.DatePicker.SelectedDate.HasValue then
            MessageBox.Show($"Your event is confirmed at {this.TimePicker.Value.Value.ToShortTimeString()} on {this.DatePicker.SelectedDate.Value.ToShortDateString()}") |> ignore
        else
            MessageBox.Show("Please enter a Date and a Time") |> ignore
