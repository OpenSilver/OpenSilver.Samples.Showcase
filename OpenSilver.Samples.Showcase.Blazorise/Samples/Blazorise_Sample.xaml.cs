using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazorise_Sample : UserControl
    {
        public Blazorise_Sample()
        {
            this.InitializeComponent();
        }

    }

    public static class Initializer
    {
        public static void AddBlazoriseSamples(this IServiceCollection services)
        {
            services.AddBlazorise(options =>
                        {
                            options.Immediate = true;
                        })
                    .AddBootstrap5Providers()
                    .AddFontAwesomeIcons();
        }
    }
}
