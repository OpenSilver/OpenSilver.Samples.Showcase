using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class MudBlazor_Sample : UserControl
    {
        public MudBlazor_Sample()
        {
            this.InitializeComponent();
        }
    }

    public static class Initializer
    {
        public static void AddMudBlazorSamples(this IServiceCollection services)
        {
            services.AddMudServices();
        }
    }
}
