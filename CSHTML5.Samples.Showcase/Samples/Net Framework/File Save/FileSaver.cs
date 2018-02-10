using System;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;

//------------------------------------
// This is an extension for C#/XAML for HTML5 (www.cshtml5.com)
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
// Copyright 2016 Userware / CSHTML5
//------------------------------------

namespace CSHTML5.Extensions.FileSystem
{
    class FileSaver
    {
        static bool JSLibraryWasLoaded;

        public static async Task SaveTextToFile(string text, string filename)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            if (filename == null)
                throw new ArgumentNullException("filename");

            if (await Initialize())
            {
                CSHTML5.Interop.ExecuteJavaScript(@"
                var blob = new Blob([$0], { type: ""text/plain;charset=utf-8""});
                saveAs(blob, $1)
            ", text, filename);
            }
        }

        public static async Task SaveJavaScriptBlobToFile(object javaScriptBlob, string filename)
        {
            if (javaScriptBlob == null)
                throw new ArgumentNullException("javaScriptBlob");

            if (filename == null)
                throw new ArgumentNullException("filename");

            if (await Initialize())
            {
                CSHTML5.Interop.ExecuteJavaScript(@"saveAs($0, $1)", javaScriptBlob, filename);
            }
        }

        static async Task<bool> Initialize()
        {
            if (CSHTML5.Interop.IsRunningInTheSimulator)
            {
                MessageBox.Show("Saving files is currently not supported in the Simulator. Please run in the browser instead.");
                return false;
            }

            if (!JSLibraryWasLoaded)
            {
                await CSHTML5.Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js");
                JSLibraryWasLoaded = true;
            }
            return true;
        }
    }
}

