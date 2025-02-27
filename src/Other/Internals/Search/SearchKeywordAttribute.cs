using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase.Search
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SearchKeywordsAttribute : Attribute
    {
        public string[] Keywords { get; }

        public SearchKeywordsAttribute(params string[] keywords)
        {
            Keywords = keywords;
        }
    }
}
