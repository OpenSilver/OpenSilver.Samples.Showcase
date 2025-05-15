using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using OpenSilver.WebAssembly;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase.Browser.Pages
{
    [Route("/")]
    public class Index : ComponentBase
    {
        [Inject]
        private LazyAssemblyLoader AssemblyLoader { get; set; }

        private class LazyLoader : ILazyLoader
        {
            private readonly LazyAssemblyLoader _assemblyLoader;

            public LazyLoader(LazyAssemblyLoader lazyLoader)
            {
                _assemblyLoader = lazyLoader;
            }

            async Task<IEnumerable<Assembly>> ILazyLoader.LoadAssembliesAsync(IEnumerable<string> assembliesToLoad)
            {
                var assemblies = await _assemblyLoader.LoadAssembliesAsync(assembliesToLoad);

                foreach (var assembly in assemblies)
                {
                    AppDomain.CurrentDomain.Load(assembly.GetName());
                }
                return assemblies;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            LazyLoadingHelper.Initialize(new LazyLoader(AssemblyLoader));
            await Runner.RunApplicationAsync<Showcase.App>();
        }
    }
}