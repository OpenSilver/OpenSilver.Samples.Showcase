using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
#if WITHBLAZOR
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
#endif

namespace OpenSilver.Samples.Showcase.Browser
{
    public class App : ComponentBase
    {
#if WITHBLAZOR
        [Inject] private ILazyFeatureNavigator NavSvc { get; set; } = default!;
        protected override void OnInitialized()
        {
            // Re-render Router when new assemblies arrive
            NavSvc.Changed += StateHasChanged;
        }

        public void Dispose()
        {
            NavSvc.Changed -= StateHasChanged;
        }
#endif

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Router>(0);
            builder.AddAttribute(1, "AppAssembly", RuntimeHelpers.TypeCheck(
                typeof(Program).Assembly
            ));

#if WITHBLAZOR
            builder.AddAttribute(2, "PreferExactMatches", RuntimeHelpers.TypeCheck(
                true
            ));
            builder.AddAttribute(3, "AdditionalAssemblies", RuntimeHelpers.TypeCheck(
                NavSvc.Assemblies
            ));
            builder.AddAttribute(4, "OnNavigateAsync",
                EventCallback.Factory.Create<NavigationContext>(
                    this,
                    (NavigationContext ctx) => NavSvc.EnsureLoadedFromPathAsync(ctx.Path)
                )
            );
#endif
            builder.AddAttribute(5, "Found", (RenderFragment<RouteData>)(routeData => builder2 =>
            {
                builder2.OpenComponent<RouteView>(6);
                builder2.AddAttribute(7, "RouteData", RuntimeHelpers.TypeCheck(
                    routeData
                ));
                builder2.CloseComponent();
            }
                ));
            builder.AddAttribute(8, "NotFound", (RenderFragment)(builder2 =>
            {
                builder2.OpenComponent<LayoutView>(9);
                builder2.AddAttribute(10, "ChildContent", (RenderFragment)(builder3 =>
                {
                    builder3.AddMarkupContent(11, "<p>Sorry, there\'s nothing at this address.</p>");
                }
                    ));
                builder2.CloseComponent();
            }
                ));
            builder.CloseComponent();
        }
    }
}