using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace OpenSilver.Samples.Showcase
{
    public static class ServiceLocator
    {
        public static IServiceProvider? Provider { get; set; }
        public static T Get<T>() where T : notnull => Provider!.GetRequiredService<T>();
    }

}
