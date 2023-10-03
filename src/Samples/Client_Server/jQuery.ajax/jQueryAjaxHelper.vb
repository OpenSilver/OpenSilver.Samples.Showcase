Imports System
Imports System.Threading.Tasks

'------------------------------------
' This extension adds "jQuery.ajax" support to C#/XAML for OpenSilver (https://opensilver.net)
'
' It is licensed under The open-source MIT license:
' https://opensource.org/licenses/MIT
'
' Copyright 2017 Userware / OpenSilver
'------------------------------------

Namespace Global.OpenSilver.Extensions
    Public Module jQueryAjaxHelper
        Public Async Function MakeAjaxCall(ByVal url As String, ByVal data As String, ByVal Optional type As String = "post") As Task(Of String)
            ' Load JQuery if it is not already loaded:
            If Not IsJQueryLoaded() Then
                Await Interop.LoadJavaScriptFile("https://code.jquery.com/jquery-3.1.1.min.js")
            End If

            ' Make the ajax call:
            Return Await MakeAjaxCall_AssumingJQueryIsLoaded(url, data, type)
        End Function

        Public Function IsJQueryLoaded() As Boolean
            Return Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof window.jQuery != 'undefined')"))
        End Function

        Private Function MakeAjaxCall_AssumingJQueryIsLoaded(ByVal url As String, ByVal data As String, ByVal Optional type As String = "post") As Task(Of String)
            ' Prepare the object that allows this method to be called using the "async/await" pattern of C#:
            Dim taskCompletionSource = New TaskCompletionSource(Of String)()

            ' Make the JavaScript call to jQuery.ajax:
            '-----------
            ' SUCCESS
            '-----------
            Interop.ExecuteJavaScript("
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
            ", url, data, type, CType(Sub(result) taskCompletionSource.SetResult(result), Action(Of String)), CType((Sub() ' This is "$0" in the code above
                                                                                                                         ' This is "$1" in the code above
                                                                                                                         ' This is "$2" in the code above
                                                                                                                         ' This is "$3" in the code above
                                                                                                                         '-----------
                                                                                                                         ' FAILURE
                                                                                                                         '-----------
                                                                                                                         If Interop.IsRunningInTheSimulator Then
                                                                                                                             taskCompletionSource.TrySetException(New Exception("An error has occurred during the call to jQuery.Ajax. For details, please run the application in the browser and view the browser console log."))
                                                                                                                         Else
                                                                                                                             taskCompletionSource.TrySetException(New Exception("An error has occurred during the call to jQuery.Ajax. A common cause is a cross-domain issue. Please view the browser console log for details (press  Ctrl+Shift+I  or  F12)."))
                                                                                                                         End If
                                                                                                                     End Sub), Action)) ' This is "$4" in the code above

            Return taskCompletionSource.Task
        End Function
    End Module
End Namespace
