using DotNetForHtml5.Core;
using Microsoft.JSInterop;
using OpenSilverBlazorServer.Wpf;
using System.Diagnostics;

namespace OpenSilverBlazorServer.Server.Initializers
{
    public class JavaScriptExecutionInitializer
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Func<Action, Task> _invokeUiAsync;
        private readonly Func<DispatcherWrapper> _dispatcherFactory;

        public JavaScriptExecutionInitializer(IJSRuntime jsRuntime, Func<Action, Task> invokeUiAsync, Func<DispatcherWrapper> dispatcherFactory)
        {
            _jsRuntime = jsRuntime;
            _invokeUiAsync = invokeUiAsync;
            _dispatcherFactory = dispatcherFactory;
        }

        public void Initialize()
        {
            INTERNAL_Simulator.IsRunningInTheSimulator_WorkAround = true;

            INTERNAL_Simulator.OpenSilverDispatcherBeginInvoke = a =>
            {
                _dispatcherFactory().BeginInvoke(() =>
                {
                    try
                    {
                        a();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception happened in the BeginInvoke. Exception: {ex}.");
                    }
                });
            };

            INTERNAL_Simulator.OpenSilverDispatcherInvoke = (a, t) =>
            {
                _dispatcherFactory().Invoke(a, t);
            };

            INTERNAL_Simulator.OpenSilverDispatcherCheckAccess = () =>
            {
                return _dispatcherFactory().CheckAccess();
            };

            var jsExecutionHandler = new JavaScriptExecutionHandler(_jsRuntime, _invokeUiAsync);

            INTERNAL_Simulator.JavaScriptExecutionHandler = jsExecutionHandler;
        }
    }
}
