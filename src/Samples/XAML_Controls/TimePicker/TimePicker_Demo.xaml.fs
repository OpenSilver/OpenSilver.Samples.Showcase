namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

type PopupMode(name: string, popup: TimePickerPopup) =
    member val Name = name with get, set
    member val Popup = popup with get, set


[<SearchKeywords("input", "selection", "range")>]
type public TimePicker_Demo() as this =
    inherit TimePicker_DemoXaml()

    do
        this.InitializeComponent()

        this.popupModes.ItemsSource <- [|
            PopupMode("List", this.timePicker.Popup)
            PopupMode("Range", RangeTimePickerPopup())
        |]
