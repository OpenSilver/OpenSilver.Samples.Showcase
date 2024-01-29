namespace TestNumericTextBox

open CSHTML5
open CSHTML5.Native.Html.Controls
open System

type NumericTextBox() as this =
    inherit HtmlPresenter()

    let mutable _value = 0

    do
        this.Html <- @"<input type=""number"" pattern=""[0-9]*"" style=""width:100%;height:100%"">"
        this.Loaded.Add(fun _ -> this.NumericTextBox_Loaded())

    member this.Value
        with get () =
            if this.DomElement <> null then //Note: the DOM element is null if the control has not been added to the visual tree yet.
                let mutable valueInt = 0
                let valueString = OpenSilver.Interop.ExecuteJavaScript("$0.value", this.DomElement) |> string
                if Int32.TryParse(valueString, &valueInt) then
                    _value <- valueInt
                _value
            else
                _value
        and set v =
            _value <- v
            if this.DomElement <> null then //Note: the DOM element is null if the control has not been added to the visual tree yet.
                OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", this.DomElement, _value) |> ignore

    member private this.NumericTextBox_Loaded() =
        // Here, the control has been added to the visual tree, so the DOM element exists. We set the initial value:
        OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", this.DomElement, _value) |> ignore
