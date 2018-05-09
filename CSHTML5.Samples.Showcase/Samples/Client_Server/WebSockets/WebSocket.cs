using System;

//------------------------------------
// This extension adds WebSocket support to C#/XAML for HTML5 (www.cshtml5.com)
//
// This project is licensed under The open-source MIT license:
// https://opensource.org/licenses/MIT
//
// Copyright 2018 Userware / CSHTML5
//------------------------------------

namespace CSHTML5.Extensions.WebSockets
{
    public class WebSocket
    {
        object _referenceToTheJavaScriptWebSocketObject;

        public event EventHandler OnOpen;
        public event EventHandler OnClose;
        public event EventHandler<OnMessageEventArgs> OnMessage;
        public event EventHandler<OnErrorEventArgs> OnError;

        public WebSocket(string uri)
        {
            _referenceToTheJavaScriptWebSocketObject = Interop.ExecuteJavaScript("new WebSocket($0)", uri);

            Interop.ExecuteJavaScript(
                    @"$0.onopen = $1;
                      $0.onclose = $2;
                      $0.onmessage = $3;
                      $0.onerror = $4",
                _referenceToTheJavaScriptWebSocketObject,
                (Action<object>)this.OnOpenCallback,
                (Action<object>)this.OnCloseCallback,
                (Action<object>)this.OnMessageCallback,
                (Action<object>)this.OnErrorCallback);
        }

        public void Send(string message)
        {
            Interop.ExecuteJavaScript("$0.send($1)", _referenceToTheJavaScriptWebSocketObject, message);
        }

        public void Close()
        {
            Interop.ExecuteJavaScript("$0.close()", _referenceToTheJavaScriptWebSocketObject);
        }

        void OnOpenCallback(object e)
        {
            if (this.OnOpen != null)
                OnOpen(this, new EventArgs());
        }

        void OnCloseCallback(object e)
        {
            if (this.OnClose != null)
                OnClose(this, new EventArgs());
        }

        void OnMessageCallback(object e)
        {
            string data = string.Empty;

            if (!Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof $0 === 'undefined')", e)))
                data = Convert.ToString(Interop.ExecuteJavaScript("$0.data", e));

            if (this.OnMessage != null)
                OnMessage(this, new OnMessageEventArgs(data));
        }

        void OnErrorCallback(object e)
        {
            string data = string.Empty;

            if (!Convert.ToBoolean(Interop.ExecuteJavaScript("(typeof $0 === 'undefined')", e)))
                data = Convert.ToString(Interop.ExecuteJavaScript("$0.data", e));

            if (this.OnError != null)
                OnError(this, new OnErrorEventArgs(data));
        }

        public ReadyState ReadyState
        {
            get
            {
                int readyStateInt = Convert.ToInt32(Interop.ExecuteJavaScript(@"$0.readyState", _referenceToTheJavaScriptWebSocketObject));
                return (ReadyState)readyStateInt;
            }
        }
    }

    public class OnMessageEventArgs : EventArgs
    {
        public readonly string Data;

        public OnMessageEventArgs(string data)
        {
            this.Data = data;
        }
    }

    public class OnErrorEventArgs : EventArgs
    {
        public readonly string Data;

        public OnErrorEventArgs(string data)
        {
            this.Data = data;
        }
    }

    public enum ReadyState : int
    {
        /// <summary>
        /// The connection is not yet open.
        /// </summary>
        CONNECTING = 0,
        /// <summary>
        /// The connection is open and ready to communicate.
        /// </summary>
        OPEN = 1,
        /// <summary>
        /// The connection is in the process of closing.
        /// </summary>
        CLOSING = 2,
        /// <summary>
        /// The connection is closed or couldn't be opened.
        /// </summary>
        CLOSED = 3
    }
}