'------------------------------------
' This extension adds WebSocket support to C#/XAML for OpenSilver (https://opensilver.net)
'
' This project is licensed under The open-source MIT license:
' https://opensource.org/licenses/MIT
'
' Copyright 2018 Userware / OpenSilver
'------------------------------------

Namespace Global.OpenSilver.Extensions.WebSockets
    Public Class WebSocket
        Private _referenceToTheJavaScriptWebSocketObject As Object

        Public Event OnOpen As EventHandler
        Public Event OnClose As EventHandler
        Public Event OnMessage As EventHandler(Of OnMessageEventArgs)
        Public Event OnError As EventHandler(Of OnErrorEventArgs)

        Public Sub New(ByVal uri As String)
            _referenceToTheJavaScriptWebSocketObject = Interop.ExecuteJavaScript("new WebSocket($0)", uri)

            Interop.ExecuteJavaScript("$0.onopen = $1;
                      $0.onclose = $2;
                      $0.onmessage = $3;
                      $0.onerror = $4", _referenceToTheJavaScriptWebSocketObject, CType(AddressOf OnOpenCallback, Action(Of Object)), CType(AddressOf OnCloseCallback, Action(Of Object)), CType(AddressOf OnMessageCallback, Action(Of Object)), CType(AddressOf OnErrorCallback, Action(Of Object)))
        End Sub

        Public Sub Send(ByVal message As String)
            Interop.ExecuteJavaScript("$0.send($1)", _referenceToTheJavaScriptWebSocketObject, message)
        End Sub

        Public Sub Close()
            Interop.ExecuteJavaScript("$0.close()", _referenceToTheJavaScriptWebSocketObject)
        End Sub

        Private Sub OnOpenCallback(ByVal e As Object)
            RaiseEvent OnOpen(Me, New EventArgs())
        End Sub

        Private Sub OnCloseCallback(ByVal e As Object)
            RaiseEvent OnClose(Me, New EventArgs())
        End Sub

        Private Sub OnMessageCallback(ByVal e As Object)
            Dim data = String.Empty

            If Not Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof $0 === 'undefined')", e)) Then data = Convert.ToString(Interop.ExecuteJavaScript("$0.data", e))

            RaiseEvent OnMessage(Me, New OnMessageEventArgs(data))
        End Sub

        Private Sub OnErrorCallback(ByVal e As Object)
            Dim data = String.Empty

            If Not Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof $0 === 'undefined')", e)) Then data = Convert.ToString(Interop.ExecuteJavaScript("$0.data", e))

            RaiseEvent OnError(Me, New OnErrorEventArgs(data))
        End Sub

        Public ReadOnly Property ReadyState As ReadyState
            Get
                Dim readyStateInt = Convert.ToInt32(Interop.ExecuteJavaScript("$0.readyState", _referenceToTheJavaScriptWebSocketObject))
                Return readyStateInt
            End Get
        End Property
    End Class

    Public Class OnMessageEventArgs
        Inherits EventArgs
        Public ReadOnly Data As String

        Public Sub New(ByVal data As String)
            Me.Data = data
        End Sub
    End Class

    Public Class OnErrorEventArgs
        Inherits EventArgs
        Public ReadOnly Data As String

        Public Sub New(ByVal data As String)
            Me.Data = data
        End Sub
    End Class

    Public Enum ReadyState As Integer
        ''' <summary>
        ''' The connection is not yet open.
        ''' </summary>
        CONNECTING = 0
        ''' <summary>
        ''' The connection is open and ready to communicate.
        ''' </summary>
        OPEN = 1
        ''' <summary>
        ''' The connection is in the process of closing.
        ''' </summary>
        CLOSING = 2
        ''' <summary>
        ''' The connection is closed or couldn't be opened.
        ''' </summary>
        CLOSED = 3
    End Enum
End Namespace
