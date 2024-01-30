namespace Ionic.Zip

open OpenSilver
open System
open System.Threading.Tasks

//------------------------------------
// This is an extension for C#/XAML for OpenSilver (https://opensilver.net)
//
// It requires v1.0 Beta 7.2 or newer.
//
// It adds the ability to create ZIP files and to read ZIP files.
// It attempts to mimic the API of Ionic.Zip (DotNetZip) so as to
// make it easier to migrate existing C# projects to CSHTML5.
//
// You can use it in conjunction with the FileSaver Extension
// to save the ZIP files to the disk. You can also use it in
// conjunction with the FileOpenDialog Extension to read a ZIP
// file from the disk.
//
// The extension works by wrapping the JavaScript "JSZip.js"
// library into a C#/XAML class for consumption by CSHTML5-based
// apps. The "JSZip.js" library source code can be found at:
// https://stuk.github.io/jszip/
//
// This extension is licensed under the open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2017 Userware / OpenSilver
//------------------------------------

type ZipFile() =
    static let mutable JSLibraryWasLoaded = false

    member val _referenceToJavaScriptZipInstance: obj = null with get, set

    member this.AddFile(fileName : string, fileContent : string) =
        async {
            do! ZipFile.LoadJSLibrary()
            this.Initialize()
            Interop.ExecuteJavaScript(@"$0.file($1, $2)", this._referenceToJavaScriptZipInstance, fileName, fileContent) |> ignore
        }

    member this.AddFile(fileName : string, fileContent : byte []) =
        async {
            do! ZipFile.LoadJSLibrary()
            this.Initialize()

#if OPENSILVER
            if not (Interop.IsRunningInTheSimulator_WorkAround) then
#else
            if not (Interop.IsRunningInTheSimulator) then
#endif
                Interop.ExecuteJavaScript(@"$0.file($1, $2)", this._referenceToJavaScriptZipInstance, fileName, fileContent) |> ignore
            else
                let length = fileContent.Length
                let array = Interop.ExecuteJavaScript("new Uint8Array($0)", length)
                for i in 0 .. length - 1 do
                    Interop.ExecuteJavaScript("$0[$1] = $2", array, i, fileContent.[i]) |> ignore
                Interop.ExecuteJavaScript(@"$0.file($1, $2)", this._referenceToJavaScriptZipInstance, fileName, array) |> ignore
        }

    member this.SaveToJavaScriptBlob() =
        async {
            do! ZipFile.LoadJSLibrary()
            this.Initialize()
            let blob = Interop.ExecuteJavaScript(@"$0.generate({type:""blob""})", this._referenceToJavaScriptZipInstance) :> obj
            return blob
        }

    interface IDisposable with
        member this.Dispose() =
            // Dispose logic goes here
            ()

    static member Read (javaScriptBlob : obj) =
        async {
            do! ZipFile.LoadJSLibrary()
            let zipFile = new ZipFile()
            zipFile._referenceToJavaScriptZipInstance <- Interop.ExecuteJavaScript("new JSZip($0)", javaScriptBlob) :> obj
            return zipFile
        }

    member this.Item (fileName : string) =
        let fileContainedInZipFile = Interop.ExecuteJavaScript(@"$0.files[$1]", this._referenceToJavaScriptZipInstance, fileName)
        let zipEntry = ZipEntry(fileContainedInZipFile)
        zipEntry

    static member private LoadJSLibrary() =
        async {
            if not JSLibraryWasLoaded then
                let! result = Interop.LoadJavaScriptFile(@"https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js") |> Async.AwaitTask
                JSLibraryWasLoaded <- true
        }

    member private this.Initialize() =
        if this._referenceToJavaScriptZipInstance = null then
            this._referenceToJavaScriptZipInstance <- Interop.ExecuteJavaScript("new JSZip()")

and ZipEntry(referenceToJSZipFile : obj) =
    member this.ExtractToString() =
        Interop.ExecuteJavaScript(@"$0.asText()", referenceToJSZipFile)

    member this.ExtractToByteArray() =
        Interop.ExecuteJavaScript(@"$0.asUint8Array()", referenceToJSZipFile)

    member this.ExtractToJavaScriptArrayBuffer() =
        Interop.ExecuteJavaScript(@"$0.asArrayBuffer()", referenceToJSZipFile)

    member this.FileName =
        Interop.ExecuteJavaScript(@"$0.name", referenceToJSZipFile)

