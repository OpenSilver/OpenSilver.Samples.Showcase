using CSHTML5.Internal;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Text.Json;

namespace OpenSilverBlazorServer.Server
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OnCallBack
    {
        private static OnCallbackSimulator OnCallBackImpl = new OnCallbackSimulator();


        [JSInvokable]
        public static void OnCallbackFromJavaScriptError(string idWhereCallbackArgsAreStored)
        {
            OnCallBackImpl.OnCallbackFromJavaScriptError(idWhereCallbackArgsAreStored);
        }

        [JSInvokable]
        public static object OnCallbackFromJavaScriptBlazorServer(
            int callbackId,
            string idWhereCallbackArgsAreStored,
            object[] callbackArgsObject,
            bool returnValue)
        {
            object result;

            try
            {
                result = OnCallBackImpl.OnCallbackFromJavaScript(
                    callbackId,
                    idWhereCallbackArgsAreStored,
                    callbackArgsObject.Select(a => a == null ? null : JavaScriptExecutionHandler.HandleResult((JsonElement)a)).ToArray(),
                    returnValue);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("DEBUG: OnCallBack: OnCallBackFromJavascript: " + ex);
                throw;
            }

            return returnValue ? result : null;
        }
    }
}
