using OpenSilver.Showcase.Other.Internals.LazyLoading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Xaml3D;

namespace OpenSilver.Showcase.Samples.Xaml3D;

public static class Initializer
{
    public static async Task InitializeXaml3D()
    {
        if (LazyAssemblyLoader.Instance != null)
        {
            await LazyAssemblyLoader.Instance.LoadAssembliesAsync(["OpenSilver.Showcase.Xaml3D.dll", "Xaml3D.dll"]);
        }

        var tcs = new TaskCompletionSource<object>();
        Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
        {
            await Root3D.Initialize();
            tcs.SetResult(null);
        });
        await tcs.Task;
    }
}
