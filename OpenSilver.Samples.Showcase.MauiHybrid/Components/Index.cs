using Microsoft.AspNetCore.Components;
using OpenSilver.MauiHybrid.Runner;

namespace OpenSilver.Samples.Showcase.MauiHybrid.Components
{
    [Route("/")]
    public class Index : ComponentBase
    {
        [Inject]
        private IMauiHybridRunner? Runner { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ArgumentNullException.ThrowIfNull(Runner);
            await Runner.RunApplicationAsync<Showcase.App>();
        }
    }
}