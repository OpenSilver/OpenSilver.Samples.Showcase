using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase
{
    public class LazyLoadingHelper
    {
        public static ILazyLoader AssemblyLoader;

        public static Assembly GetLoadedAssembly(string assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == assemblyName);
        }
    }
    public interface ILazyLoader
    {
        Task<IEnumerable<Assembly>> LoadAssembliesAsync(IEnumerable<string> assembliesToLoad);
    }
}
