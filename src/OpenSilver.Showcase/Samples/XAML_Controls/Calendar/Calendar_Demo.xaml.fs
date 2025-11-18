namespace OpenSilver.Showcase

open System
open System.Windows
open System.Windows.Controls
open OpenSilver.Showcase.Search
open System.Globalization

[<SearchKeywords("input", "calendar", "date", "selection", "schedule", "picker", "control")>]
type public Calendar_Demo() as this =
    inherit Calendar_DemoXaml()

    do
        this.InitializeComponent()

    member private this.OnPastDatesChanged(_sender: obj, _e: RoutedEventArgs) =
        if not (isNull this.sampleCalendar) then
            let isChecked = this.chkPastDateSelection.IsChecked.GetValueOrDefault()
            if isChecked then
                this.sampleCalendar.BlackoutDates.Clear()
            else
                try
                    this.sampleCalendar.BlackoutDates.AddDatesInPast()
                with _ ->
                    this.chkPastDateSelection.IsChecked <- Nullable(true)

    member private this.OnCultureChanged(sender: obj, e: SelectionChangedEventArgs) =
        match this.culturesCombo.SelectedItem with
        | :? ComboBoxItem as selected ->
            match selected.Tag with
            | :? string as tag ->
                let culture = CultureInfo(tag)
                this.globalCalendar.CalendarInfo <- CultureCalendarInfo(culture)
            | _ -> ()
        | _ -> ()
