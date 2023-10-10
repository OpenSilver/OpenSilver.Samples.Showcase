Imports CSHTML5.Native.Html.Controls
Imports System
Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
#Else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#End If

'------------------------------------
' This is an extension for C#/XAML for OpenSilver (https://opensilver.net)
'
' It requires v1.0 Beta 7.2 or newer.
'
' It adds the ability to let the user pick a file using the
' "Open File..." dialog of the browser.
'
' It can also be used for example in conjunction with the ZipFile
' Extension to load a ZIP file. Note: if you are looking for
' a way to save files to disk, you can use the FileSaver Extension.
'
' The extension works by adding an <input type='file'> tag to the
' HTML DOM, which will cause the "Open file..." dialog to appear,
' and by listening to its "change" event in order to get a reference
' to the selected file.
'
' This extension is licensed under the open-source MIT license:
' https://opensource.org/licenses/MIT
'
' Copyright 2017 Userware / OpenSilver
'------------------------------------

Namespace Global.OpenSilver.Extensions.FileOpenDialog
    Public Class ControlForDisplayingTheFileOpenDialog
        Inherits HtmlPresenter
        Public Event FileOpened As EventHandler(Of FileOpenedEventArgs)

        Private _resultKind As ResultKind
        Private _resultKindStr As String
        Public Property ResultKind As ResultKind
            Get
                Return _resultKind
            End Get
            Set(ByVal value As ResultKind)
                _resultKind = value
                _resultKindStr = value.ToString()
            End Set
        End Property

        Public Sub New()
            ResultKind = ResultKind.Text 'Note: this is to set the default value of the property.

            Html = "<input type='file'>"

            AddHandler Loaded, AddressOf ControlForDisplayingAFileOpenDialog_Loaded
        End Sub

        Private Sub ControlForDisplayingAFileOpenDialog_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Get the reference to the "input" element:
            Dim inputElement = Interop.GetDiv(Me)

            Dim onFileOpened As Action(Of Object) = Sub(result) RaiseEvent FileOpened(Me, New FileOpenedEventArgs(result, ResultKind))
            Dim resultKindStr = _resultKindStr ' Note: this is here to "capture" the variable so that it can be used inside the "addEventListener" below.

            ' Apply the "Filter" property to limit the choice of the user to the specified extensions:
            SetFilter(Filter)

            ' Listen to the "change" property of the "input" element, and call the callback:
            Interop.ExecuteJavaScript("
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
                    var resultKind = $2;
                    if (resultKind == 'DataURL') {
                      reader.readAsDataURL(file);
                    }
                    else {
                      reader.readAsText(file);
                    }
                });", inputElement, onFileOpened, resultKindStr)
        End Sub

        Private Sub SetFilter(ByVal filter As String)
            ' Get the reference to the "input" element:
            Dim inputElement = Interop.GetDiv(Me)

            ' Process the filter list to convert the syntax from XAML to HTML5:
            ' Example of syntax in Silverlight: Image Files (*.bmp, *.jpg)|*.bmp;*.jpg|All Files (*.*)|*.*
            ' Example of syntax in HTML5: .gif, .jpg, .png, .doc
            Dim splitted = filter.Split("|"c)
            Dim itemsKept As List(Of String) = New List(Of String)()
            If splitted.Length = 1 Then
                itemsKept.Add(splitted(0))
            Else
                For i = 1 To splitted.Length - 1 Step 2
                    itemsKept.Add(splitted(i))
                Next
            End If
            Dim filtersInHtml5 = String.Join(",", itemsKept).Replace("*", "").Replace(";", ",")

            ' Apply the filter:
            If Not String.IsNullOrWhiteSpace(filtersInHtml5) Then
                Interop.ExecuteJavaScript("$0.accept = $1", inputElement, filtersInHtml5)
            Else
                Interop.ExecuteJavaScript("$0.accept = """"", inputElement)
            End If
        End Sub


        Public Property Filter As String
            Get
                Return CStr(GetValue(FilterProperty))
            End Get
            Set(ByVal value As String)
                SetValue(FilterProperty, value)
            End Set
        End Property
        Public Shared ReadOnly FilterProperty As DependencyProperty = DependencyProperty.Register("Filter", GetType(String), GetType(ControlForDisplayingTheFileOpenDialog), New PropertyMetadata("", AddressOf Filter_Changed))

        Private Shared Sub Filter_Changed(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim control = CType(d, ControlForDisplayingTheFileOpenDialog)
            If CSharpXamlForHtml5.DomManagement.IsControlInVisualTree(control) Then
                control.SetFilter(If(e.NewValue, "").ToString())
            End If
        End Sub
    End Class

    Public Class FileOpenedEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' Only available if the property "ResultKind" was set to "Text"
        ''' </summary>
        <Obsolete>
        Public ReadOnly PlainTextContent As String

        ''' <summary>
        ''' Only available if the property "ResultKind" was set to "Text".
        ''' </summary>
        Public ReadOnly Text As String

        ''' <summary>
        ''' Only available if the property "ResultKind" was set to "DataURL".
        ''' </summary>
        Public ReadOnly DataURL As String

        Public Sub New(ByVal result As Object, ByVal resultKind As ResultKind)
            If resultKind = ResultKind.Text Then
                Text = CSharpImpl.__Assign(PlainTextContent, If(result, "").ToString())
            ElseIf resultKind = ResultKind.DataURL Then
                DataURL = If(result, "").ToString()
            Else
                Throw New NotSupportedException()
            End If
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Enum ResultKind
        Text
        DataURL
    End Enum
End Namespace
