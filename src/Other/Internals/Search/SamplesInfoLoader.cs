using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Maui.Devices;

namespace OpenSilver.Samples.Showcase.Search
{
    public static class SamplesInfoLoader
    {
        //keep a mapping of the name to the type, so we can later instantiate the type after a search.
        private static Dictionary<string, Type> controlTypeMap = new Dictionary<string, Type>();

        public static List<SearchableItem> GetAllControls()
        {
            var controls = new List<SearchableItem>();

            var types = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(t => t.IsClass && t.GetCustomAttribute<SearchKeywordsAttribute>() != null);

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<SearchKeywordsAttribute>();
                var name = type.Name;
                bool ignoreType = DeviceInfo.Current.Platform == DevicePlatform.Unknown && attribute?.Keywords.Contains("maui") == true;
                if (!ignoreType)
                controls.Add(new SearchableItem
                {
                    Name = name,
                    Keywords = attribute?.Keywords.ToList() ?? new List<string>()
                });
                controlTypeMap[name] = type;
            }

            return controls;
        }

        public static Type GetControlTypeByName(string name)
        {
            return controlTypeMap.TryGetValue(name, out var type) ? type : null;
        }
    }
}
