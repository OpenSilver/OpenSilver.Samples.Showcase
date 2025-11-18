using Microsoft.Extensions.DependencyInjection;
using DevExpress.Blazor;
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

namespace OpenSilver.Showcase
{
    public partial class DevExpress_Sample : UserControl
    {
        public DevExpress_Sample()
        {
            this.InitializeComponent();
        }
    }
    
    public static class Initializer
    {
        public static void AddDevExpressSamples(this IServiceCollection services)
        {
            services.AddDevExpressBlazor();
        }
    }
}
