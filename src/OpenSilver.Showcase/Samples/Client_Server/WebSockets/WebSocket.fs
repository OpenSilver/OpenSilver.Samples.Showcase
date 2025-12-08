namespace OpenSilver.Extensions.WebSockets

open System
open OpenSilver

//------------------------------------
// This extension adds WebSocket support to C#/XAML for OpenSilver (https://opensilver.net)
//
// This project is licensed under The open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2018 Userware / OpenSilver
//------------------------------------

type WebSocket(uri: string) as this=

    let mutable _referenceToTheJavaScriptWebSocketObject : obj = null
    let eventOnOpen = new Event<EventArgs>()
    let eventOnClose = new Event<EventArgs>()
    let eventOnMessage = new Event<OnMessageEventArgs>()
    let eventOnError = new Event<OnErrorEventArgs>()

    do
        _referenceToTheJavaScriptWebSocketObject <- Interop.ExecuteJavaScript("new WebSocket($0)", uri)
        
        let openCallBackDelegate : Delegate = upcast Func<obj, unit>(fun obj -> this.OnOpenCallback(obj))
        let closeCallBackDelegate : Delegate = upcast Func<obj, unit>(fun obj -> this.OnCloseCallback(obj))
        let messageCallBackDelegate : Delegate = upcast Func<obj, unit>(fun obj -> this.OnMessageCallback(obj))
        let errorCallBackDelegate : Delegate = upcast Func<obj, unit>(fun obj -> this.OnErrorCallback(obj))

        Interop.ExecuteJavaScript(@"
            $0.onopen = $1;
            $0.onclose = $2;
            $0.onmessage = $3;
            $0.onerror = $4",
            _referenceToTheJavaScriptWebSocketObject, openCallBackDelegate, closeCallBackDelegate, messageCallBackDelegate, errorCallBackDelegate) |> ignore

    member this.OnOpen = eventOnOpen.Publish
    member this.OnClose = eventOnClose.Publish
    member this.OnMessage = eventOnMessage.Publish
    member this.OnError = eventOnError.Publish

    member this.Send(message : string)=
        Interop.ExecuteJavaScript("$0.send($1)", _referenceToTheJavaScriptWebSocketObject, message) |> ignore

    member this.OnOpenCallback(e: obj) =
        eventOnOpen.Trigger(EventArgs())

    member this.OnCloseCallback(e: obj) =
        eventOnClose.Trigger(EventArgs())

    member this.OnMessageCallback(e: obj) =
        let mutable data = ""
        if not (Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof $0 === 'undefined')", e))) then
            data <- Convert.ToString(Interop.ExecuteJavaScript("$0.data", e))

        eventOnMessage.Trigger(OnMessageEventArgs(data))

    member this.OnErrorCallback(e: obj) =
        let mutable data = ""
        if not (Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof $0 === 'undefined')", e))) then
            data <- Convert.ToString(Interop.ExecuteJavaScript("$0.data", e))

        eventOnError.Trigger(OnErrorEventArgs(data))

    member this.ReadyState : ReadyState =
        let readyStateInt = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.readyState", _referenceToTheJavaScriptWebSocketObject))
        LanguagePrimitives.EnumOfValue<int , ReadyState>(readyStateInt)

and OnMessageEventArgs(data: string) =
    member this.Data = data

and OnErrorEventArgs(data: string) =
    member this.Data = data

and ReadyState =
    | CONNECTING = 0
    | OPEN = 1
    | CLOSING = 2
    | CLOSED = 3
