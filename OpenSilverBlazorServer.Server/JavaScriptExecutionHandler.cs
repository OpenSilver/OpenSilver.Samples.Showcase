using DotNetForHtml5;
using Microsoft.JSInterop;
using System.Text.Json;

namespace OpenSilverBlazorServer.Server
{
    public class JavaScriptExecutionHandler : IJavaScriptExecutionHandler
    {
        private readonly IJSRuntime _jsRuntime;

        private readonly Func<Action, Task> _invokeUiAsync;

        public JavaScriptExecutionHandler(IJSRuntime jsRuntime, Func<Action, Task> invokeUiAsync)
        {
            _jsRuntime = jsRuntime;
            _invokeUiAsync = invokeUiAsync;
        }

        public void ExecuteJavaScript(string javaScriptToExecute)
        {
            _invokeUiAsync(() =>
            {
                _jsRuntime.InvokeVoidAsync("executeJs", javaScriptToExecute);
            });
        }

        private async Task<object> InvokeWithResult(string javaScriptToExecute)
        {
            var res = await _jsRuntime.InvokeAsync<object>("executeJs", javaScriptToExecute);

            Console.WriteLine("Result on server " + res);

            return res;
        }

        public static object HandleResult(JsonElement result)
        {
            switch (result.ValueKind)
            {
                case JsonValueKind.String:
                    string stringValue = result.GetString();
                    // Handle string result
                    return stringValue;
                    break;
                case JsonValueKind.Number:
                    // If you expect the number could be decimal, use GetDecimal() or another appropriate method
                    double numberValue = result.GetDouble();
                    // Handle number result
                    return numberValue;
                    break;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    bool boolValue = result.GetBoolean();
                    // Handle boolean result
                    return boolValue;
                    break;
                default:
                    // Handle other cases or unknown result
                    return null;
                    break;
            }
        }

        public object ExecuteJavaScriptWithResult(string javaScriptToExecute)
        {
            var tcs = new TaskCompletionSource<object?>();

            _invokeUiAsync(async () =>
            {
                var js = javaScriptToExecute;
                var res = await _jsRuntime.InvokeAsync<JsonElement>("executeJs", js);
                //var json = ((JsonElement)res).GetRawText();
                tcs.SetResult(HandleResult(res));
            });

            return tcs.Task.Result;
        }
    }
}
