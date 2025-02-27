using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSilver.Samples.Showcase.Search
{
    public static class ControlSearch
    {
        private static List<SearchableItem> controls = SamplesInfoLoader.GetAllControls();

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
