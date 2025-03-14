using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OpenSilver.MauiHybrid.Runner;

namespace OpenSilver.Samples.Showcase.MauiHybrid.Components
{
    [Route("/")]
    public class Index : ComponentBase
    {
        [Inject]
        private IMauiHybridRunner? Runner { get; set; }

        [Inject]
        public required IJSRuntime JS { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ArgumentNullException.ThrowIfNull(Runner);
            await Runner.RunApplicationAsync<Showcase.App>();

#if MACCATALYST // workaround to prevent displaying text too bold on Mac
            await JS.InvokeVoidAsync("eval", "document.body.style.fontSynthesis = 'none'");
#endif
            // seems like WebKit does not allow links with '_blank' target,
            // so replace it with '_self' to be handled by BlazorWebView
#if IOS || MACCATALYST
            await JS.InvokeVoidAsync("eval",
@"(function() {
        const originalWindowOpen = window.open;
        window.open = function(url, target, features) {
            if (target === '_blank') {
                target = '_self';
            }
            return originalWindowOpen(url, target, features);
        };
    })();");
#endif
        }
    }
}