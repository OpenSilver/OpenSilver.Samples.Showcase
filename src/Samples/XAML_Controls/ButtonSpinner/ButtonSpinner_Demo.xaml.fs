namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("updown")>]
type public ButtonSpinner_Demo() as this =
    inherit ButtonSpinner_DemoXaml()

    let months =
        [| "January"; "February"; "March"; "April"; "May"; "June"
           "July"; "August"; "September"; "October"; "November"; "December" |]

    let mutable monthIndex = 0

    do
        this.InitializeComponent()
        this.UpdateSpinnerContent()

    member private this.OnButtonSpinnerSpin(_sender: obj, e: SpinEventArgs) =
        monthIndex <-
            if e.Direction = SpinDirection.Increase then monthIndex + 1 else monthIndex - 1

        if monthIndex < 0 then
            monthIndex <- months.Length - 1
        elif monthIndex >= months.Length then
            monthIndex <- 0

        this.UpdateSpinnerContent()

    member private this.UpdateSpinnerContent() =
        this.spinner.Content <- months.[monthIndex]
