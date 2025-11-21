using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using OpenSilver.WebAssembly;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenSilver.Showcase.Browser.Pages
{
    [Route("/")]
    public class Index : ComponentBase
    {
        [Inject]
        private LazyAssemblyLoader AssemblyLoader { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Other.Internals.LazyLoading.LazyAssemblyLoader.Instance = new LazyLoader(AssemblyLoader);
            await Runner.RunApplicationAsync<Showcase.App>();
        }

        private class LazyLoader(LazyAssemblyLoader lazyLoader) : Other.Internals.LazyLoading.ILazyAssemblyLoader
        {
            private readonly LazyAssemblyLoader _assemblyLoader = lazyLoader;

            Task<IEnumerable<Assembly>> Other.Internals.LazyLoading.ILazyAssemblyLoader.LoadAssembliesAsync(IEnumerable<string> assembliesToLoad)
                => _assemblyLoader.LoadAssembliesAsync(assembliesToLoad);
        }
    }
}