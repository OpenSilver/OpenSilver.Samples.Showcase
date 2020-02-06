using System;
using System.Threading.Tasks;

//------------------------------------
// This extension adds "jQuery.ajax" support to C#/XAML for HTML5 (www.cshtml5.com)
//
// It is licensed under The open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2017 Userware / CSHTML5
//------------------------------------

namespace CSHTML5.Extensions
{
    public static class jQueryAjaxHelper
    {
        public static async Task<string> MakeAjaxCall(string url, string data, string type = "post")
        {
            // Load JQuery if it is not already loaded:
            if (!IsJQueryLoaded())
            {
                await Interop.LoadJavaScriptFile("https://code.jquery.com/jquery-3.1.1.min.js");
            }

            // Make the ajax call:
            return await MakeAjaxCall_AssumingJQueryIsLoaded(url, data, type);
        }

        public static bool IsJQueryLoaded()
        {
            return Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof window.jQuery != 'undefined')"));
        }

        private static Task<string> MakeAjaxCall_AssumingJQueryIsLoaded(string url, string data, string type = "post")
        {
            // Prepare the object that allows this method to be called using the "async/await" pattern of C#:
            var taskCompletionSource = new TaskCompletionSource<string>();

            // Make the JavaScript call to jQuery.ajax:
            Interop.ExecuteJavaScript(@"
                window.jQuery.ajax({
                    url: $0, 
                type: $2,      
                data: ""html="" + $1,     
                cache: false,
                success: function(returnhtml){                          
                    ($3)(returnhtml);
                    },
                error: function(xhr, status, error) {
                    ($4)();
                    }
                });
            ",
            url, // This is "$0" in the code above
            data, // This is "$1" in the code above
            type, // This is "$2" in the code above
            (Action<string>)(result => // This is "$3" in the code above
            {
                //-----------
                // SUCCESS
                //-----------
                taskCompletionSource.SetResult(result);
            }),
            (Action)(() => // This is "$4" in the code above
            {
                //-----------
                // FAILURE
                //-----------
                if (Interop.IsRunningInTheSimulator)
                    taskCompletionSource.TrySetException(new Exception("An error has occurred during the call to jQuery.Ajax. For details, please run the application in the browser and view the browser console log."));
                else
                    taskCompletionSource.TrySetException(new Exception("An error has occurred during the call to jQuery.Ajax. A common cause is a cross-domain issue. Please view the browser console log for details (press  Ctrl+Shift+I  or  F12)."));
            }));

            return taskCompletionSource.Task;
        }
    }
}