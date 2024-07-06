namespace OpenSilver.Extensions.FileOpenDialog

open System
open System.Windows
open System.Windows.Controls
open CSHTML5.Native.Html.Controls
open OpenSilver
open CSHTML5.Internal

//------------------------------------
// This is an extension for C#/XAML for OpenSilver (https://opensilver.net)
//
// It requires v1.0 Beta 7.2 or newer.
//
// It adds the ability to let the user pick a file open the
// "Open File..." dialog of the browser.
//
// It can also be used for example in conjunction with the ZipFile
// Extension to load a ZIP file. Note: if you are looking for
// a way to save files to disk, you can use the FileSaver Extension.
//
// The extension works by adding an <input type='file'> tag to the
// HTML DOM, which will cause the "Open file..." dialog to appear,
// and by listening to its "change" event in order to get a reference
// to the selected file.
//
// This extension is licensed under the open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2017 Userware / OpenSilver
//------------------------------------

type ResultKind =
    | Text
    | DataURL

type FileOpenedEventArgs(result : obj, resultKind : ResultKind) =
    inherit EventArgs()

    let mutable plainTextContent = ""
    let mutable text = ""
    let mutable dataURL = ""

    do
        if resultKind = ResultKind.Text then
            text <- result :?> string
            plainTextContent <- text
        elif resultKind = ResultKind.DataURL then
            dataURL <- result :?> string
        else
            failwith "Unsupported resultKind"

    member this.PlainTextContent with get() = plainTextContent and set(value) = plainTextContent <- value
    member this.Text with get() = text and set(value) = text <- value
    member this.DataURL with get() = dataURL and set(value) = dataURL <- value

type ControlForDisplayingTheFileOpenDialog() as this =
    inherit HtmlPresenter()

    let fileOpenedEvent = new Event<FileOpenedEventArgs>()

    let mutable _resultKind = ResultKind.Text
    let mutable _resultKindStr = ""

    do
        this.ResultKind <- ResultKind.Text

        this.Html <- "<input type='file'>"

        this.ScrollMode <- ScrollMode.Disabled

        this.Loaded.Add(fun args -> this.ControlForDisplayingAFileOpenDialog_Loaded(args))

    [<CLIEvent>]
    member this.FileOpened = fileOpenedEvent.Publish

    member this.ResultKind
        with get() = _resultKind 
        and set(value) = 
            _resultKind <- value
            _resultKindStr <- value.ToString()

    member private this.ControlForDisplayingAFileOpenDialog_Loaded(e: RoutedEventArgs) =
        // Get the reference to the "input" element:
        let inputElement = Interop.GetDiv(this)

        let onFileOpened result = fileOpenedEvent.Trigger(FileOpenedEventArgs(result, this.ResultKind))

        let resultKindStr = _resultKindStr // Note: this is here to "capture" the variable so that it can be used inside the "addEventListener" below.

        // Apply the "Filter" property to limit the choice of the user to the specified extensions:
        this.SetFilter(this.Filter)

        // Listen to the "change" property of the "input" element, and call the callback:
        OpenSilver.Interop.ExecuteJavaScript(@"
            $0.addEventListener(""change"", function(e) {
                if(!e) {
                    e = window.event;
                }
                var fullPath = $0.value;
                var filename = '';
                if (fullPath) {
                    var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
                    filename = fullPath.substring(startIndex);
                    if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) {
                        filename = filename.substring(1);
                    }
                }
                var input = e.target;
                var file = input.files[0];
                var reader = new FileReader();
                reader.onload = function() {
                    var callback = $1;
                    var result = reader.result;
                    //if (file.type == 'text/plain')
                    callback(result);
                };
                var resultKind = $2;
                if (resultKind == 'DataURL') {
                    reader.readAsDataURL(file);
                }
                else {
                    reader.readAsText(file);
                }
            });", inputElement, onFileOpened, resultKindStr) |> ignore

    member private this.SetFilter(filter : string)=
        // Get the reference to the "input" element:
        let inputElement = OpenSilver.Interop.GetDiv(this)

        // Process the filter list to convert the syntax from XAML to HTML5:
        // Example of syntax in Silverlight: Image Files (*.bmp, *.jpg)|*.bmp;*.jpg|All Files (*.*)|*.*
        // Example of syntax in HTML5: .gif, .jpg, .png, .doc
        let splitted = filter.Split('|')
        let itemsKept =
            if splitted.Length = 1 then [splitted.[0]]
            else
                seq { for i in 1 .. 2 .. (splitted.Length - 1) -> splitted.[i] }
                |> List.ofSeq

        let filtersInHtml5 = String.Join(",", itemsKept).Replace("*", "").Replace(";", ",")

        // Apply the filter:
        if not (String.IsNullOrWhiteSpace filtersInHtml5) then
            OpenSilver.Interop.ExecuteJavaScript("$0.accept = $1", inputElement, filtersInHtml5) |> ignore
        else
            OpenSilver.Interop.ExecuteJavaScript("$0.accept = ''", inputElement) |> ignore

    static member FilterProperty =
        DependencyProperty.Register("Filter", typeof<string>, typeof<ControlForDisplayingTheFileOpenDialog>, new PropertyMetadata(defaultValue = "", MethodToUpdateDom = ControlForDisplayingTheFileOpenDialog.Filter_Changed))

    member this.Filter
        with get() = (this.GetValue(ControlForDisplayingTheFileOpenDialog.FilterProperty) :?> string)
        and set(value : string) = this.SetValue(ControlForDisplayingTheFileOpenDialog.FilterProperty, value)

    static member Filter_Changed (d: DependencyObject) (newValue : obj) =
        let control = d :?> ControlForDisplayingTheFileOpenDialog
        if INTERNAL_VisualTreeManager.IsElementInVisualTree(control) then
            control.SetFilter(if isNull(newValue) then "" else newValue :?> string)
