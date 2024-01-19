namespace OpenSilver.Extensions.FileSystem

open System
open System.Threading.Tasks
open System.Windows
#if !SLMIGRATION
open Windows.UI.Xaml
#endif

//------------------------------------
// This is an extension for C#/XAML for OpenSilver (https://opensilver.net)
//
// It requires Beta 7.2 or newer.
//
// It adds the ability to save files locally via the "Save As..."
// dialog of the browser. Please note that some browsers will
// directly save the files to the "Downloads" folder without
// asking the user.
//
// The extension works by wrapping the JavaScript "FileSaver.js"
// library into a C#/XAML class for consumption by CSHTML5-based
// apps. The "FileSaver.js" library source code can be found at:
// https://github.com/eligrey/FileSaver.js/
//
// This extension is licensed under the open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2016 Userware / OpenSilver
//------------------------------------

open System
open System.Threading.Tasks

type FileSaver() =
    static let mutable JSLibraryWasLoaded = false

    static member private initialize () : Async<bool> =
        async {
#if OPENSILVER
            if OpenSilver.Interop.IsRunningInTheSimulator_WorkAround then
#else
            if isRunningInSimulator = OpenSilver.Interop.IsRunningInTheSimulator then
#endif
                MessageBox.Show("Saving files is currently not supported in the Simulator. Please run in the browser instead.") |> ignore
                return false
            else
                if not JSLibraryWasLoaded then
                    OpenSilver.Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js") |> Async.AwaitTask |> ignore
                    JSLibraryWasLoaded <- true
                return true
        }

    static member SaveTextToFile(text: string, filename: string) =
        async {
            if text = null then
                ArgumentNullException("text") |> ignore
            elif filename = null then
                ArgumentNullException("filename") |> ignore
            else
                let! initialized = FileSaver.initialize()
                if initialized then
                    OpenSilver.Interop.ExecuteJavaScript(@"
                var blob = new Blob([$0], { type: ""text/plain;charset=utf-8""});
                saveAs(blob, $1)", text, filename) |> ignore
        }

    static member SaveJavaScriptBlobToFile(javaScriptBlob: obj, filename: string) =
        async {
            if javaScriptBlob = null then
                ArgumentNullException("javaScriptBlob") |> ignore
            elif filename = null then
                ArgumentNullException("filename") |> ignore
            else
                let! initialized = FileSaver.initialize()
                if initialized then
                    OpenSilver.Interop.ExecuteJavaScript(@"saveAs($0, $1)", javaScriptBlob, filename) |> ignore
        }
