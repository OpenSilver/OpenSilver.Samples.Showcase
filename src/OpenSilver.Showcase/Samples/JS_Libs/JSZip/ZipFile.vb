Imports OpenSilver
Imports OpenSilver.Showcase

'------------------------------------
' This is an extension for C#/XAML for OpenSilver (https://opensilver.net)
'
' It requires v1.0 Beta 7.2 or newer.
'
' It adds the ability to create ZIP files and to read ZIP files.
' It attempts to mimic the API of Ionic.Zip (DotNetZip) so as to
' make it easier to migrate existing C# projects to CSHTML5.
'
' You can use it in conjunction with the FileSaver Extension
' to save the ZIP files to the disk. You can also use it in
' conjunction with the FileOpenDialog Extension to read a ZIP
' file from the disk.
'
' The extension works by wrapping the JavaScript "JSZip.js"
' library into a C#/XAML class for consumption by CSHTML5-based
' apps. The "JSZip.js" library source code can be found at:
' https://stuk.github.io/jszip/
'
' This extension is licensed under the open-source MIT license:
' https://opensource.org/licenses/MIT
'
' Copyright 2017 Userware / OpenSilver
'------------------------------------

Namespace Global.Ionic.Zip
    Public Class ZipFile
        Private Shared ReadOnly _jsLibraryLoaded As Task(Of Boolean) = FileLoader.TryLoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js")

        Private _referenceToJavaScriptZipInstance As Object

        Public Async Function AddFile(ByVal fileName As String, ByVal fileContent As String) As Task
            If Not Await _jsLibraryLoaded Then
                Return
            End If

            Initialize()

            Interop.ExecuteJavaScript("$0.file($1, $2)", _referenceToJavaScriptZipInstance, fileName, fileContent)
        End Function

        Public Async Function AddFile(ByVal fileName As String, ByVal fileContent As Byte()) As Task
            If Not Await _jsLibraryLoaded Then
                Return
            End If

            Initialize()

            If Not Interop.IsRunningInTheSimulator Then
                Interop.ExecuteJavaScript("$0.file($1, $2)", _referenceToJavaScriptZipInstance, fileName, fileContent)
            Else
                Dim length = fileContent.Length
                Dim array = Interop.ExecuteJavaScript("new Uint8Array($0)", length)
                Dim i = 0

                While i < length
                    Interop.ExecuteJavaScript("$0[$1] = $2", array, i, fileContent(i))
                    Threading.Interlocked.Increment(i)
                End While
                Interop.ExecuteJavaScript("$0.file($1, $2)", _referenceToJavaScriptZipInstance, fileName, array)
            End If
        End Function

        Public Async Function SaveToJavaScriptBlob() As Task(Of Object)
            If Not Await _jsLibraryLoaded Then
                Return Nothing
            End If

            Initialize()

            Dim blob = Interop.ExecuteJavaScript("$0.generate({type:""blob""})", _referenceToJavaScriptZipInstance)

            Return blob
        End Function

        Private Sub Initialize()
            _referenceToJavaScriptZipInstance = If(_referenceToJavaScriptZipInstance, Interop.ExecuteJavaScript("new JSZip()"))
        End Sub

        Public Shared Async Function Read(ByVal javaScriptBlob As Object) As Task(Of ZipFile)
            If Not Await _jsLibraryLoaded Then
                Return Nothing
            End If

            Dim zipFile = New ZipFile()

            zipFile._referenceToJavaScriptZipInstance = Interop.ExecuteJavaScript("new JSZip($0)", javaScriptBlob)

            Return zipFile
        End Function

        Default Public ReadOnly Property Item(ByVal fileName As String) As ZipEntry
            Get
                Dim fileContainedInZipFile = Interop.ExecuteJavaScript("$0.files[$1]", _referenceToJavaScriptZipInstance, fileName)
                Dim zipEntry = New ZipEntry(fileContainedInZipFile)
                Return zipEntry
            End Get
        End Property
    End Class

    Public Class ZipEntry
        Private ReadOnly _referenceToJSZipFile As Object

        Public Sub New(referenceToJSZipFile As Object)
            _referenceToJSZipFile = referenceToJSZipFile
        End Sub

        Public Function ExtractToString() As String
            Return Interop.ExecuteJavaScriptGetResult(Of String)($"{_referenceToJSZipFile}.asText()")
        End Function

        Public Function ExtractToByteArray() As Byte()
            Return Interop.ExecuteJavaScriptGetResult(Of Byte())($"{_referenceToJSZipFile}.asUint8Array()")
        End Function

        Public Function ExtractToJavaScriptArrayBuffer() As Object
            Return Interop.ExecuteJavaScript("$0.asArrayBuffer()", _referenceToJSZipFile)
        End Function

        Public ReadOnly Property FileName As String
            Get
                Return Interop.ExecuteJavaScriptGetResult(Of String)($"{_referenceToJSZipFile}.name")
            End Get
        End Property
    End Class

End Namespace
