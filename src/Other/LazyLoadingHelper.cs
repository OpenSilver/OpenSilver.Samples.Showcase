using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OpenSilver.Samples.Showcase;

public interface ILazyLoader
{
    Task<IEnumerable<Assembly>> LoadAssembliesAsync(IEnumerable<string> assembliesToLoad);
}

public class LazyLoadingHelper
{
    private static ILazyLoader _assemblyLoader;

    private static readonly HashSet<string> _assembliesToLoad =
    [
        "Microsoft.Maui.Essentials",
        "Microsoft.Maui.Graphics"
    ];

    private const string AssemblyExtension = ".dll";

    public static void Initialize(ILazyLoader assemblyLoader)
    {
        _assemblyLoader = assemblyLoader ?? throw new ArgumentNullException(nameof(assemblyLoader));

        //AppDomain.CurrentDomain.TypeResolve += OnAssemblyResolve;
    }

    private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
    {
        Console.WriteLine($"Attempting to resolve: {args.Name} {args.RequestingAssembly?.FullName}");

        var assemblyName = new AssemblyName(args.Name).Name;
        Assembly assembly = null;

        if (_assembliesToLoad.Contains(assemblyName))
        {
            var resetEvent = new ManualResetEventSlim();
            var id = Environment.CurrentManagedThreadId;
            Dispatcher.CurrentDispatcher.InvokeAsync(async () =>
            {
                var z = Environment.CurrentManagedThreadId;
                try
                {
                    var assemblies = await _assemblyLoader.LoadAssembliesAsync([assemblyName + AssemblyExtension]);
                    assembly = assemblies.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading assembly: {ex}");
                }
                finally
                {
                    resetEvent.Set();
                }
            });
            resetEvent.Wait(); // Wait for the async task to finish
        }

        return assembly;
    }

    public static async Task LoadMauiAssemblies()
    {
        if (_assemblyLoader == null)
        {
            return;
        }
        await _assemblyLoader.LoadAssembliesAsync(
        [
            "Microsoft.Maui.Essentials.dll",
            "Microsoft.Maui.Graphics.dll"
        ]);
    }

    //public static void LoadMauiAssemblies()
    //{
    //    var httpClient = new HttpClient();
    //    //httpClient.get
    //    //LoadAssemblies([
    //    //    "Microsoft.Maui.Essentials.dll",
    //    //    "Microsoft.Maui.Graphics.dll"
    //    //]);
    //}

    //private static void LoadAssemblies(IEnumerable<string> assemblies)
    //{
    //    if (assemblies == null || !assemblies.Any())
    //    {
    //        throw new ArgumentException("No assemblies to load.", nameof(assemblies));
    //    }

    //    var assembliesToLoad = new List<string>();
    //    var allAssemblies = new HashSet<string>(AppDomain.CurrentDomain..GetAssemblies().Select(a => a.GetName().Name));
    //    foreach (var assembly in assemblies)
    //    {
    //        var name = Path.GetFileNameWithoutExtension(assembly);
            
    //        if (!allAssemblies.Contains(name))
    //        {
    //            assembliesToLoad.Add(assembly);
    //        }
    //    }

    //    if (assembliesToLoad.Count > 0)
    //    {
    //        var resetEvent = new ManualResetEventSlim();
    //        Dispatcher.CurrentDispatcher.InvokeAsync(async () =>
    //        {
    //            try
    //            {
    //                var loadedAssemblies = await _assemblyLoader.LoadAssembliesAsync(assembliesToLoad);
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"Error loading assembly: {ex}");
    //            }
    //            finally
    //            {
    //                resetEvent.Set();
    //            }
    //        });
    //        resetEvent.Wait(); // Wait for the async task to finish
    //    }
    //}
}
