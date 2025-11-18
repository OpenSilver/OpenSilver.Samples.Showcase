namespace TestNumericTextBox

open System
open CSHTML5.Internal
open CSHTML5.Native.Html.Controls
open OpenSilver

type NumericTextBox() as this =
    inherit HtmlPresenter()

    let mutable value = 0
    let mutable domElement: obj = null

    do
        this.Html <- "<input type='number' pattern='[0-9]*' style='width:100%;height:100%'>"
        this.Loaded.Add(fun _ -> this.NumericTextBox_Loaded())

    member this.Value
        with get() =
            if not (isNull domElement) then
                let id = (domElement :?> INTERNAL_HtmlDomElementReference).UniqueIdentifier
                let valueStr = Interop.ExecuteJavaScriptGetResult<string>($"{id}.firstChild.firstChild.value")
                match Int32.TryParse(valueStr) with
                | (true, parsedValue) -> value <- parsedValue
                | _ -> ()
            value
        and set(v) =
            value <- v
            if not (isNull domElement) then
                this.UpdateValue()

    member private this.NumericTextBox_Loaded() =
        domElement <- Interop.GetDiv(this)
        this.UpdateValue()

    member private this.UpdateValue() =
        Interop.ExecuteJavaScriptVoidAsync("$0.firstChild.firstChild.value = $1", domElement, value)
