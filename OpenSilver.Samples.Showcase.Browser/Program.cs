using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenSilver.Blazor;
using OpenSilver.Samples.Showcase;

namespace OpenSilver.Samples.Showcase.Browser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // For blazor components
            builder.UseBlazorForOpenSilver();
            builder.Services.AddRadzenSamples();

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}