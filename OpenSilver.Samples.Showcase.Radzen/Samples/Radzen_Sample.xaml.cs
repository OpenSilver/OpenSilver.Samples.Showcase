using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace OpenSilver.Samples.Showcase
{
    public partial class Radzen_Sample : UserControl
    {
        public Radzen_Sample()
        {
            InitializeComponent();
        }
    }

    public static class Initializer
    {
        public static void AddRadzenSamples(this IServiceCollection services)
        {
            services.AddRadzenComponents();
        }
    }
}

