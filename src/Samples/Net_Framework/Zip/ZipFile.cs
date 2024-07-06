using CSHTML5;
using System;
using System.Threading.Tasks;

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

namespace Ionic.Zip
{
    public class ZipFile : IDisposable
    {
        static bool JSLibraryWasLoaded;

        object _referenceToJavaScriptZipInstance;

        public async Task AddFile(string fileName, string fileContent)
        {
            await LoadJSLibrary();

            Initialize();

            Interop.ExecuteJavaScript(@"$0.file($1, $2)", _referenceToJavaScriptZipInstance, fileName, fileContent);
        }

        public async Task AddFile(string fileName, Byte[] fileContent)
        {
            await LoadJSLibrary();

            Initialize();

            if (!OpenSilver.Interop.IsRunningInTheSimulator)
            {
                Interop.ExecuteJavaScript(@"$0.file($1, $2)", _referenceToJavaScriptZipInstance, fileName, fileContent);
            }
            else
            {
                int length = fileContent.Length;
                var array = Interop.ExecuteJavaScript("new Uint8Array($0)", length);
                for (int i = 0; i < length; ++i)
                {
                    Interop.ExecuteJavaScript("$0[$1] = $2", array, i, fileContent[i]);
                }
                Interop.ExecuteJavaScript(@"$0.file($1, $2)", _referenceToJavaScriptZipInstance, fileName, array);
            }
        }

        public async Task<object> SaveToJavaScriptBlob()
        {
            await LoadJSLibrary();

            Initialize();

            object blob = Interop.ExecuteJavaScript(@"$0.generate({type:""blob""})", _referenceToJavaScriptZipInstance);

            return blob;
        }

        public void Dispose()
        {
        }

        static async Task LoadJSLibrary()
        {
            if (!JSLibraryWasLoaded)
            {
                await Interop.LoadJavaScriptFile(@"https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js");
                JSLibraryWasLoaded = true;
            }
        }

        void Initialize()
        {
            if (_referenceToJavaScriptZipInstance == null)
                _referenceToJavaScriptZipInstance = Interop.ExecuteJavaScript("new JSZip()");
        }

        public static async Task<ZipFile> Read(object javaScriptBlob)
        {
            await LoadJSLibrary();

            var zipFile = new ZipFile();

            zipFile._referenceToJavaScriptZipInstance = Interop.ExecuteJavaScript("new JSZip($0)", javaScriptBlob);

            return zipFile;
        }

        public ZipEntry this[string fileName]
        {
            get
            {
                var fileContainedInZipFile = Interop.ExecuteJavaScript(@"$0.files[$1]", _referenceToJavaScriptZipInstance, fileName);
                var zipEntry = new ZipEntry(fileContainedInZipFile);
                return zipEntry;
            }
        }
    }


    public class ZipEntry
    {
        object _referenceToJSZipFile;

        public ZipEntry(object referenceToJSZipFile)
        {
            _referenceToJSZipFile = referenceToJSZipFile;
        }

        public string ExtractToString()
        {
            return (string)Interop.ExecuteJavaScript(@"$0.asText()", _referenceToJSZipFile);
        }

        public Byte[] ExtractToByteArray()
        {
            return (Byte[])Interop.ExecuteJavaScript(@"$0.asUint8Array()", _referenceToJSZipFile);

        }

        public object ExtractToJavaScriptArrayBuffer()
        {
            return Interop.ExecuteJavaScript(@"$0.asArrayBuffer()", _referenceToJSZipFile);
        }

        public string FileName
        {
            get
            {
                return (string)Interop.ExecuteJavaScript(@"$0.name", _referenceToJSZipFile);
            }
        }
    }
}