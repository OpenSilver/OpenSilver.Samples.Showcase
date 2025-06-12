using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase
{
    public class PageInfo : IMenuElement
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsVisibleInMenu { get; set; }
    }
}
