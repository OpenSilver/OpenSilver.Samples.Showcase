Imports System.Windows

'------------------------------------
' This is an extension for C#/XAML for OpenSilver (https://opensilver.net)
'
' It requires Beta 7.2 or newer.
'
' It adds the ability to save files locally via the "Save As..."
' dialog of the browser. Please note that some browsers will
' directly save the files to the "Downloads" folder without
' asking the user.
'
' The extension works by wrapping the JavaScript "FileSaver.js"
' library into a C#/XAML class for consumption by CSHTML5-based
' apps. The "FileSaver.js" library source code can be found at:
' https://github.com/eligrey/FileSaver.js/
'
' This extension is licensed under the open-source MIT license:
' https://opensource.org/licenses/MIT
'
' Copyright 2016 Userware / OpenSilver
'------------------------------------

Namespace Global.OpenSilver.Extensions.FileSystem
    Friend Class FileSaver
        Private Shared JSLibraryWasLoaded As Boolean

        Public Shared Async Function SaveTextToFile(ByVal text As String, ByVal filename As String) As Task
            If Equals(text, Nothing) Then Throw New ArgumentNullException("text")

            If Equals(filename, Nothing) Then Throw New ArgumentNullException("filename")

            If Await Initialize() Then
                Interop.ExecuteJavaScript("
                var blob = new Blob([$0], { type: ""text/plain;charset=utf-8""});
                saveAs(blob, $1)
            ", text, filename)
            End If
        End Function

        Public Shared Async Function SaveJavaScriptBlobToFile(ByVal javaScriptBlob As Object, ByVal filename As String) As Task
            If javaScriptBlob Is Nothing Then Throw New ArgumentNullException("javaScriptBlob")

            If Equals(filename, Nothing) Then Throw New ArgumentNullException("filename")

            If Await Initialize() Then
                Interop.ExecuteJavaScript("saveAs($0, $1)", javaScriptBlob, filename)
            End If
        End Function

        Private Shared Async Function Initialize() As Task(Of Boolean)
            If OpenSilver.Interop.IsRunningInTheSimulator Then
                MessageBox.Show("Saving files is currently not supported in the Simulator. Please run in the browser instead.")
                Return False
            End If

            If Not JSLibraryWasLoaded Then
                Await Interop.LoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.min.js")
                JSLibraryWasLoaded = True
            End If
            Return True
        End Function
    End Class
End Namespace
