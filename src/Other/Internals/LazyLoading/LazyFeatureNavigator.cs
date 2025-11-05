#if WITHBLAZOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

namespace OpenSilver.Samples.Showcase
{
    using System.Reflection;

    public interface ILazyFeatureNavigator
    {
        IReadOnlyList<Assembly> Assemblies { get; }
        event Action? Changed;

        Task EnsureLoadedFromPathAsync(string path);
        Task NavigateToAsync(string featureKey, string? sub = null);
    }


    public sealed class LazyFeatureNavigator : ILazyFeatureNavigator
    {
        private readonly LazyAssemblyLoader _loader;
        private readonly NavigationManager _nav;
        private readonly List<Assembly> _assemblies = new();
        public IReadOnlyList<Assembly> Assemblies => _assemblies;
        public event Action? Changed;

        // Adjust dll lists to your app; names must match blazor.boot.json
        private readonly Dictionary<string, string[]> _dlls = new(StringComparer.OrdinalIgnoreCase)
        {
            [LazyLoadingConstants.SYNCFUSION_NAME] = LazyLoadingConstants.SYNCFUSION_DLLs,
            [LazyLoadingConstants.DEVEXPRESS_NAME] = LazyLoadingConstants.DEVEXPRESS_DLLS
        };

        public LazyFeatureNavigator(LazyAssemblyLoader loader, NavigationManager nav)
        {
            _loader = loader; _nav = nav;
        }

        public async Task NavigateToAsync(string featureKey, string? sub = null)
        {
            await EnsureLoadedAsync(featureKey);
            _nav.NavigateTo($"/{featureKey}{(string.IsNullOrEmpty(sub) ? "" : "/" + sub)}");
        }

        public Task EnsureLoadedFromPathAsync(string path)
        {
            return EnsureLoadedAsync(path);

            //if (path.StartsWith(LazyLoadingConstants.SYNCFUSION_NAME, StringComparison.OrdinalIgnoreCase)) return EnsureLoadedAsync(LazyLoadingConstants.SYNCFUSION_NAME);
            //else if (path.StartsWith(LazyLoadingConstants.DEVEXPRESS_NAME, StringComparison.OrdinalIgnoreCase)) return EnsureLoadedAsync(LazyLoadingConstants.DEVEXPRESS_NAME);
            //return Task.CompletedTask;
        }

        private async Task EnsureLoadedAsync(string featureKey)
        {
            if (!_dlls.TryGetValue(featureKey, out var dlls)) return;

            var loaded = await _loader.LoadAssembliesAsync(dlls);
            var added = false;
            foreach (var asm in loaded)
                if (!_assemblies.Contains(asm)) { _assemblies.Add(asm); added = true; }

            if (added) Changed?.Invoke();
        }
    }

}

#endif