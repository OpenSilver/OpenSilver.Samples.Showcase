namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Threading.Tasks
open CSHTML5.Native.Html.Controls
open OpenSilver

type JsParticlesEffect() as this =
    inherit HtmlPresenter()

    static let mutable isJsLibLoaded = false
    let mutable domElement : obj = null

    do
        this.Loaded.AddHandler(RoutedEventHandler(fun s e -> this.OnLoaded(s, e) |> ignore))
        this.Unloaded.AddHandler(RoutedEventHandler(fun s e -> this.OnUnloaded(s, e)))

    member private this.OnLoaded(sender: obj, e: RoutedEventArgs) =
        task {
            this.Html <- "<div></div>"
            domElement <- Interop.GetDiv(this)
            do! JsParticlesEffect.LoadJSLibrary()
            do! Task.Delay(100)

            //do! Interop.ExecuteJavaScriptVoidAsync($"
            //    $0.firstChild.style.width = '100%';
            //    $0.firstChild.style.height = '100%';
            //    $0.firstChild.firstChild.style.width = '100%';
            //    $0.firstChild.firstChild.style.height = '100%';
            //    window.ParticleEffect.startEffect($0.firstChild.firstChild);
            //", domElement)
            //|> Async.AwaitTask
        } (*|> Async.StartImmediate*)

    member private this.OnUnloaded(sender: obj, e: RoutedEventArgs) =
        Interop.ExecuteJavaScriptVoidAsync($"
            window.ParticleEffect.stopEffect();
        ", domElement)
        |> ignore

    static member private LoadJSLibrary() : Task =
        task {
            if not isJsLibLoaded then
                //do! Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js")
                Interop.ExecuteJavaScriptVoid("...full JS content here...")
                isJsLibLoaded <- true
        }

