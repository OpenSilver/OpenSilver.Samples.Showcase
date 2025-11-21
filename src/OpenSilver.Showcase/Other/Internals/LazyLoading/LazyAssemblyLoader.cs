using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenSilver.Showcase.Other.Internals.LazyLoading;

public interface ILazyAssemblyLoader
{
    Task<IEnumerable<Assembly>> LoadAssembliesAsync(IEnumerable<string> assembliesToLoad);
}

public static class LazyAssemblyLoader
{
    public static ILazyAssemblyLoader Instance { get; set; }
}
