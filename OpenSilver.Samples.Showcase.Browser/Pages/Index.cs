using DotNetForHtml5;
using OpenSilver.Samples.Showcase.Browser.Interop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using System.Reflection;
using System.Collections.Generic;

namespace OpenSilver.Samples.Showcase.Browser.Pages
{
    [Route("/")]
    public class Index : ComponentBase
    {
        [Inject]
        private LazyAssemblyLoader AssemblyLoader { get; set; }

        private class LazyLoader : ILazyLoader
        {
            private LazyAssemblyLoader _assemblyLoader;

            public LazyLoader(LazyAssemblyLoader lazyLoader)
            {
                _assemblyLoader = lazyLoader;
            }

            Task<IEnumerable<Assembly>> ILazyLoader.LoadAssembliesAsync(IEnumerable<string> assembliesToLoad)
            {
                return _assemblyLoader.LoadAssembliesAsync(assembliesToLoad);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (!await JSRuntime.InvokeAsync<bool>("getOSFilesLoadedPromise"))
            {
                throw new InvalidOperationException("Failed to initialize OpenSilver. Check your browser's console for error details.");
            }

            LazyLoadingHelper.AssemblyLoader = new LazyLoader(AssemblyLoader);

            Cshtml5Initializer.Initialize(new UnmarshalledJavaScriptExecutionHandler(JSRuntime));
            Program.RunApplication();
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }
    }
}