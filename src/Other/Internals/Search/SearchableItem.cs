using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase.Search
{
    /// <summary>
    /// Represents data used to search for a Sample by the search function.
    /// </summary>
    public class SearchableItem
    {
        /// <summary>
        /// The name of the sample to search for.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The keywords the sample can be associated with.
        /// </summary>
        public List<string> Keywords { get; set; }
    }
}
