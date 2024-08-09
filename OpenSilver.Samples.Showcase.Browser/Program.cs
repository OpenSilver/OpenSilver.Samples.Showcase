using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Threading;

namespace OpenSilver.Samples.Showcase.Browser
{
    public class Program
    {
        static WebAssemblyHostBuilder builder;
        public static async Task Main(string[] args)
        {
            builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // For blazor components
            OpenSilver.Compatibility.Blazor.Initializer.Initialize(builder);
            builder.Services.AddScoped<global::Radzen.DialogService>();


            var host = builder.Build();
            await host.RunAsync();
        }

        public static void RunApplication()
        {
            Application.RunApplication(() =>
            {
                var app = new OpenSilver.Samples.Showcase.App();
            });
        }


    }
}