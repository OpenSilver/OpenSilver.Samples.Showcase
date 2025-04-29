namespace OpenSilver.Samples.Showcase

open CSHTML5.Native.Html.Controls
open System
open System.Threading.Tasks
open System.Windows
open OpenSilver

type ToastUiEditor() as this =
    inherit HtmlPresenter()
    
    static let mutable _isJsLibLoaded = false
    static let CdnUrl = "https://uicdn.toast.com/editor/latest"
    let mutable _domElement: obj = null
    let mutable _contentInEditor: string = ""
    
    // Content Property
    //static let OnContentChanged (d: DependencyObject) (e: DependencyPropertyChangedEventArgs) =
    //    let editor = d :?> ToastUiEditor
    //    let newContent = e.NewValue :?> string
        
        //if editor._domElement <> null && newContent <> editor._contentInEditor then
        //    editor._contentInEditor <- newContent
        //    Interop.ExecuteJavaScriptVoidAsync(
        //        "if($0.editor) { $0.editor.setMarkdown($1); }",
        //        editor._domElement,
        //        newContent
        //    ) |> ignore
    
    static let ContentProperty: DependencyProperty =
        DependencyProperty.Register("Content", typeof<string>, typeof<ToastUiEditor>, 
            PropertyMetadata(String.Empty))
    
    member this.Content
        with get() = this.GetValue(ContentProperty) :?> string
        and set(value: string) = this.SetValue(ContentProperty, value)
      // Load JS Library
    static member private LoadJSLibrary(): Task =
        async {
            if not _isJsLibLoaded then
                //do! Interop.LoadCssFile($"{CdnUrl}/toastui-editor.min.css") |> Async.AwaitTask
                //do! Interop.LoadJavaScriptFile($"{CdnUrl}/toastui-editor-all.min.js") |> Async.AwaitTask
                _isJsLibLoaded <- true
        } |> Async.StartAsTask :> Task
    
    // Event handlers
    //let OnContentChangedInEditor(newContent: string) =
    //    _contentInEditor <- newContent
    //    this.Content <- newContent
    
    //let OnLoaded(_: obj) (e: RoutedEventArgs) =
    //    //this.Loaded.RemoveHandler(RoutedEventHandler(OnLoaded))
        
    //    async {
    //        do! ToastUiEditor.LoadJSLibrary() |> Async.AwaitTask
    //        _domElement <- Interop.GetDiv(this)
            
    //        //do! Interop.ExecuteJavaScriptAsync(
    //        //    "$0.editor = new toastui.Editor({" +
    //        //    "    el: $0.firstChild," +
    //        //    "    initialValue: $1," +
    //        //    "    previewStyle: 'vertical'," +
    //        //    "    initialEditType: 'wysiwyg'," +
    //        //    "    theme: 'dark'" +
    //        //    "});" +
    //        //    "" +
    //        //    "$0.editor.on('change', () => {" +
    //        //    "    const value = $0.editor.getMarkdown();" +
    //        //    "    $2(value);" +
    //        //    "});", 
    //        //    _domElement, this.Content, Action<string>(OnContentChangedInEditor)) |> Async.AwaitTask
    //    } |> Async.StartImmediate
    
    //do
    //    this.Loaded.AddHandler(RoutedEventHandler(OnLoaded))
