namespace OpenSilver.Extensions

open System
open System.Threading.Tasks
open OpenSilver

//------------------------------------
// This extension adds "jQuery.ajax" support to C#/XAML for OpenSilver (https://opensilver.net)
//
// It is licensed under The open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2017 Userware / OpenSilver
//------------------------------------

type jQueryAjaxHelper() =

    static member private IsJQueryLoaded() =
        Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof window.jQuery != 'undefined')"))

    static member MakeAjaxCall (url: string, data: string, ?typeStr: string) =
        async {
            // Load JQuery if it is not already loaded:
            if not (jQueryAjaxHelper.IsJQueryLoaded()) then
                Interop.LoadJavaScriptFile("https://code.jquery.com/jquery-3.1.1.min.js") |> ignore
            // Make the ajax call:
            return! jQueryAjaxHelper.MakeAjaxCall_AssumingJQueryIsLoaded(url, data, defaultArg typeStr "post")
        }

    static member private MakeAjaxCall_AssumingJQueryIsLoaded (url: string, data: string, ?typeStr: string) =
        async {
            // Prepare the object that allows this method to be called open the "async/await" pattern of C#:
            let taskCompletionSource = new TaskCompletionSource<string>()

            // Make the JavaScript call to jQuery.ajax:
            Interop.ExecuteJavaScript(@"
                window.jQuery.ajax({
                    url: $0, 
                    type: $2,      
                    data: ""html="" + $1,     
                    cache: false,
                    success: function(returnhtml){                          
                        ($3)(returnhtml)
                    },
                    error: function(xhr, status, error) {
                        ($4)()
                    }
                })
            ",
                url, // This is "$0" in the code above
                data, // This is "$1" in the code above
                typeStr, // This is "$2" in the code above
                box (fun (result: string) -> // This is "$3" in the code above
                    // SUCCESS
                    taskCompletionSource.SetResult(result)
                ),
                box (fun () -> // This is "$4" in the code above
                    // FAILURE
                    if Interop.IsRunningInTheSimulator then
                        taskCompletionSource.TrySetException(new Exception("An error has occurred during the call to jQuery.Ajax. For details, please run the application in the browser and view the browser console log."))
                    else
                        taskCompletionSource.TrySetException(new Exception("An error has occurred during the call to jQuery.Ajax. A common cause is a cross-domain issue. Please view the browser console log for details (press Ctrl+Shift+I or F12)."))
                )
            ) |> ignore

            return! taskCompletionSource.Task |> Async.AwaitTask
        }

