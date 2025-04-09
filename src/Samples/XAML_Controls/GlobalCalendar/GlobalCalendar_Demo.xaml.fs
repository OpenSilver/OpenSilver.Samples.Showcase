namespace OpenSilver.Samples.Showcase

open System.Globalization
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "date", "selection", "schedule", "picker", "control", "culture", "globalization")>]
type GlobalCalendar_Demo() as this =
    inherit GlobalCalendar_DemoXaml()
    do this.InitializeComponent()

    member private this.OnCultureChanged(sender: obj, e: SelectionChangedEventArgs) =
        match this.culturesCombo.SelectedItem with
        | :? ComboBoxItem as selected ->
            match selected.Tag with
            | :? string as tag ->
                let culture = CultureInfo(tag)
                this.globalCalendar.CalendarInfo <- CultureCalendarInfo(culture)
            | _ -> ()
        | _ -> ()
