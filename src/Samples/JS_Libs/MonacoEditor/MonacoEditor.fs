namespace OpenSilver.Samples.Showcase

open System
open System.Threading.Tasks
open System.Windows
open System.Windows.Media
open OpenSilver
open CSHTML5.Native.Html.Controls

type MonacoEditor() as this =
    inherit HtmlPresenter()

    static let cdnUrl = "https://unpkg.com/monaco-editor@latest/min/vs"
    static let mutable isJsLibLoaded = false

    let mutable domElement: obj = null

    static let codeProperty =
        DependencyProperty.Register(
            "Code",
            typeof<string>,
            typeof<MonacoEditor>,
            PropertyMetadata(String.Empty)
        )
        
    static let languageProperty =
        DependencyProperty.Register(
            "Language",
            typeof<string>,
            typeof<MonacoEditor>,
            PropertyMetadata("plaintext")
        )

    member this.Code
        with get() = this.GetValue(codeProperty) :?> string
        and set(value: string) = this.SetValue(codeProperty, value)

    member this.Language
        with get() = this.GetValue(languageProperty) :?> string
        and set(value: string) = this.SetValue(languageProperty, value)
        
    //// Shadow the base Language property
    //member private this.OnCodeChangedInEditor(newCode: string) =
    //    this.Code <- newCode

    //member private this.domElement
    //    with get() = domElement
    //    and set(value) = domElement <- value

    //static member private LoadJSLibrary() =
    //    task {
    //        if not isJsLibLoaded then
    //            do! Interop.LoadJavaScriptFile($"{cdnUrl}/loader.js") |> ignore
    //            isJsLibLoaded <- true
    //    }    
        
//    member private this.OnLoaded(sender: obj, e: RoutedEventArgs) =
//        //this.Loaded.RemoveHandler(RoutedEventHandler(this.OnLoaded))

//        async {
//            do! MonacoEditor.LoadJSLibrary() |> Async.AwaitTask
            
//            domElement <- Interop.GetDiv(this)

//            this.Html <- $"<div></div>"
            
////            do! Interop.ExecuteJavaScriptAsync(
////                $"require.config({{
////    paths: {{ 'vs': '{cdnUrl}' }}
////}});                
////require(['vs/editor/editor.main'], function() {{
////        $0.editor = monaco.editor.create($0.firstChild.firstChild, {{
////            value: $1,
////            language: $2,
////            theme: 'vs-dark',
////            minimap: {{
////                enabled: false
////            }},
////            automaticLayout: true
////        }});

////        $0.editor.onDidChangeModelContent((e) => {{
////            const value = $0.editor.getValue();
////            $3(value);
////        }});
////    }});", domElement, this.Code, this.Language, Action<string>(this.OnCodeChangedInEditor))
////            |> Async.AwaitTask
//        } |> Async.StartImmediate

    //do
    //    this.Loaded.AddHandler(RoutedEventHandler(this.OnLoaded))

    static member CodeProperty = codeProperty
    static member LanguageProperty = languageProperty
