using System;
using System.Collections.Generic;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

//------------------------------------
// This is an extension for C#/XAML for HTML5 (www.cshtml5.com)
//
// It requires Beta 7.2 or newer.
//
// It adds the ability to let the user pick a file using the
// "Open File..." dialog of the browser.
//
// It can also be used for example in conjunction with the ZipFile
// Extension to load a ZIP file. Note: if you are looking for
// a way to save files to disk, you can use the FileSaver Extension.
//
// The extension works by adding an <input type='file'> tag to the
// HTML DOM, which will cause the "Open file..." dialog to appear,
// and by listening to its "change" event in order to get a reference
// to the selected file. it returns a JavaScript blob. If the file
// is a plain text file, it will also return a string with the text
// content of the file.
//
// This extension is licensed under the open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2017 Userware / CSHTML5
//------------------------------------

namespace CSHTML5.Extensions.FileOpenDialog
{
    public class ControlForDisplayingTheFileOpenDialog : Control
    {
        public event EventHandler<FileOpenedEventArgs> FileOpened;

        private ResultKind _resultKind;
        private string _resultKindStr;
        public ResultKind ResultKind
        {
            get { return _resultKind; }
            set
            {
                _resultKind = value;
                _resultKindStr = value.ToString();
            }
        }

        public ControlForDisplayingTheFileOpenDialog()
        {
            ResultKind = FileOpenDialog.ResultKind.Text; //Note: this is to set the default value of the property.

            CSharpXamlForHtml5.DomManagement.SetHtmlRepresentation(this,
                "<input type='file'>");

            this.Loaded += ControlForDisplayingAFileOpenDialog_Loaded;
        }

        void ControlForDisplayingAFileOpenDialog_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the reference to the "input" element:
            var inputElement = CSHTML5.Interop.GetDiv(this);

            Action<object> onFileOpened = (result) =>
            {
                if (this.FileOpened != null)
                    this.FileOpened(this, new FileOpenedEventArgs(result, this.ResultKind));
            };
            string resultKindStr = _resultKindStr; // Note: this is here to "capture" the variable so that it can be used inside the "addEventListener" below.

            // Apply the "Filter" property to limit the choice of the user to the specified extensions:
            SetFilter(this.Filter);

            // Listen to the "change" property of the "input" element, and call the callback:
            CSHTML5.Interop.ExecuteJavaScript(@"
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
                    var resultKind = $3;
                    if (resultKind == 'DataURL') {
                      reader.readAsDataURL(file);
                    }
                    else {
                      reader.readAsText(file);
                    }
                    var isRunningInTheSimulator = $2;
                    if (isRunningInTheSimulator) {
                        alert(""The file open dialog is not supported in the Simulator. Please test in the browser instead."");
                    }
                });", inputElement, onFileOpened, CSHTML5.Interop.IsRunningInTheSimulator, resultKindStr);
        }

        void SetFilter(string filter)
        {
            // Get the reference to the "input" element:
            var inputElement = CSHTML5.Interop.GetDiv(this);

            // Process the filter list to convert the syntax from XAML to HTML5:
            // Example of syntax in Silverlight: Image Files (*.bmp, *.jpg)|*.bmp;*.jpg|All Files (*.*)|*.*
            // Example of syntax in HTML5: .gif, .jpg, .png, .doc
            string[] splitted = filter.Split('|');
            List<string> itemsKept = new List<string>();
            if (splitted.Length == 1)
            {
                itemsKept.Add(splitted[0]);
            }
            else
            {
                for (int i = 1; i < splitted.Length; i += 2)
                {
                    itemsKept.Add(splitted[i]);
                }
            }
            string filtersInHtml5 = String.Join(",", itemsKept).Replace("*", "").Replace(";", ",");

            // Apply the filter:
            if (!string.IsNullOrWhiteSpace(filtersInHtml5))
            {
                CSHTML5.Interop.ExecuteJavaScript(@"$0.accept = $1", inputElement, filtersInHtml5);
            }
            else
            {
                CSHTML5.Interop.ExecuteJavaScript(@"$0.accept = """"", inputElement);
            }
        }


        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(ControlForDisplayingTheFileOpenDialog), new PropertyMetadata("", Filter_Changed));

        static void Filter_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ControlForDisplayingTheFileOpenDialog)d;
            if (CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(control))
            {
                control.SetFilter((e.NewValue ?? "").ToString());
            }
        }
    }

    public class FileOpenedEventArgs : EventArgs
    {
        /// <summary>
        /// Only available if the property "ResultKind" was set to "Text"
        /// </summary>
        [Obsolete]
        public readonly string PlainTextContent;

        /// <summary>
        /// Only available if the property "ResultKind" was set to "Text".
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Only available if the property "ResultKind" was set to "DataURL".
        /// </summary>
        public readonly string DataURL;

        public FileOpenedEventArgs(object result, ResultKind resultKind)
        {
            if (resultKind == ResultKind.Text)
            {
                this.Text = this.PlainTextContent = (result ?? "").ToString();
            }
            else if (resultKind == ResultKind.DataURL)
            {
                this.DataURL = (result ?? "").ToString();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }

    public enum ResultKind
    {
        Text, DataURL
    }
}