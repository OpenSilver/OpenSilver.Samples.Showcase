namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

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
