using System.Collections.Generic;
using System.Linq;

namespace OpenSilver.Samples.Showcase.Search
{
    public static class ControlSearch
    {
        private static List<SearchableItem> controls = SamplesInfoLoader.GetAllControls();

        public static readonly string[] SearchKeywords = controls
            .SelectMany(x => x.Keywords.Append(x.Name))
            .Select(x => x.ToLower().Replace("_demo", ""))
            .Distinct()
            .OrderBy(x => x)
            .ToArray();

        public static List<SearchableItem> Search(string query)
        {
            query = query.ToLower(); // Case insensitive

            return controls.Where(control =>
                control.Name.ToLower().Contains(query) ||  // Match control name
                control.Keywords.Any(k => k.ToLower().Contains(query)) // Match keywords
            ).ToList();
        }
    }
}
