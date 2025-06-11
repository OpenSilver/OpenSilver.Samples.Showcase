using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenSilver.Samples.Showcase
{
    public static class UIElementHelpers
    {
        public static Task WaitForLoadedAsync(FrameworkElement element)
        {
            if (element.IsLoaded)
            {
                return Task.CompletedTask;
            }

            var tcs = new TaskCompletionSource<object>();
            RoutedEventHandler handler = null;

            handler = (s, e) =>
            {
                element.Loaded -= handler;
                tcs.SetResult(null);
            };

            element.Loaded += handler;
            return tcs.Task;
        }

    }
}
